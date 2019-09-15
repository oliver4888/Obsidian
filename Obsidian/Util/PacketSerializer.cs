using Obsidian.Chat;
using Obsidian.Commands;
using Obsidian.Logging;
using Obsidian.Net;
using Obsidian.Net.Packets;
using Obsidian.PlayerData.Info;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Obsidian.Util
{
    public static class PacketSerializer
    {
        private static readonly Logger logger = new Logger("Serialzer", LogLevel.Debug);

        private static async Task<object> ReadAsync(MinecraftStream stream, Variable var)
        {
            switch (var.Type)
            {
                case VariableType.Int: return await stream.ReadIntAsync();
                case VariableType.Long: return await stream.ReadLongAsync();
                case VariableType.VarInt: return await stream.ReadVarIntAsync();
                case VariableType.VarLong: return await stream.ReadVarLongAsync();
                case VariableType.UnsignedByte: return await stream.ReadUnsignedByteAsync();
                case VariableType.Byte: return await stream.ReadByteAsync();
                case VariableType.Short: return await stream.ReadShortAsync();
                case VariableType.UnsignedShort: return await stream.ReadUnsignedShortAsync();
                case VariableType.String: return await stream.ReadStringAsync();
                case VariableType.Array: return await stream.ReadUInt8ArrayAsync(var.Attribute.Size);
                case VariableType.ByteArray: return await stream.ReadUInt8ArrayAsync(var.Attribute.Size);
                case VariableType.Position: return await stream.ReadPositionAsync();
                case VariableType.Boolean: return await stream.ReadBooleanAsync();
                case VariableType.Float: return await stream.ReadFloatAsync();
                case VariableType.Double: return await stream.ReadDoubleAsync();
                case VariableType.Transform: return await stream.ReadTransformAsync();

                default:
                case VariableType.List: logger.LogWarning("type not supported..."); return null;//TODO: Add list VariableType
            }
        }

        private static async Task WriteAsync(MinecraftStream stream, Variable var, object value)
        {
            switch (var.Type)
            {
                case VariableType.Int: await stream.WriteIntAsync((int)value); break;
                case VariableType.Long: await stream.WriteLongAsync((long)value); break;
                case VariableType.VarInt: await stream.WriteVarIntAsync((int)value); break;
                case VariableType.VarLong: await stream.WriteVarLongAsync((long)value); break;
                case VariableType.UnsignedByte: await stream.WriteUnsignedByteAsync((byte)value); break;
                case VariableType.Byte: await stream.WriteByteAsync((sbyte)value); break;
                case VariableType.Short: await stream.WriteShortAsync((short)value); break;
                case VariableType.UnsignedShort: await stream.WriteUnsignedShortAsync((ushort)value); break;
                case VariableType.String: await stream.WriteStringAsync((string)value); break;
                case VariableType.Position: await stream.WritePositionAsync((Position)value); break;
                case VariableType.Boolean: await stream.WriteBooleanAsync((bool)value); break;
                case VariableType.Float: await stream.WriteFloatAsync((float)value); break;
                case VariableType.Double: await stream.WriteDoubleAsync((double)value); break;
                case VariableType.Chat: await stream.WriteChatAsync((ChatMessage)value); break;
                case VariableType.ByteArray: await stream.WriteAsync((byte[])value); break;

                default:
                case VariableType.Transform: //TODO: add writing transforms
                case VariableType.Array: logger.LogWarning($"Failed to read type: {var.Type}..."); break;//TODO: add writing int arrays
                case VariableType.List:
                    var list = (IList)value;
                    var arg = list.GetType().GetGenericArguments()[0];

                    //Checking what the list takes
                    if (arg.GetType() == typeof(CommandNode))
                    {
                        logger.LogDebug("Command Node list");
                        var nodes = list.Cast<CommandNode>().ToList();

                        foreach (var obj in nodes)
                        {
                            /*var type = obj.Type;
                            using var mcStream = new MinecraftStream();

                            await mcStream.WriteByteAsync((sbyte)type);
                            await mcStream.WriteVarIntAsync(obj.Children.Count);

                            foreach (var child in obj.Children)
                                await mcStream.WriteVarIntAsync(child.Index);

                            if (type.HasFlag(CommandNodeType.HasRedirect))
                                await stream.WriteVarIntAsync(0);

                            if (type.HasFlag(CommandNodeType.Argument) || type.HasFlag(CommandNodeType.Literal))
                            {
                                if (!string.IsNullOrWhiteSpace(obj.Name))
                                    await stream.WriteStringAsync(obj.Name);
                            }

                            if (type.HasFlag(CommandNodeType.Argument))
                                await obj.Parser.WriteAsync(mcStream);

                            await mcStream.CopyToAsync(stream);*/

                            var nodeArray = await obj.ToArrayAsync();

                            await stream.WriteAsync(nodeArray);
                        }
                    }
                    else if (arg.GetType() == typeof(PlayerInfoAction))
                    {
                        logger.LogDebug("PlayerInfoAction  list");
                        var actions = list.Cast<PlayerInfoAction>().ToList();

                        foreach (var action in actions)
                            await action.WriteAsync(stream);
                    }

                    break;
            }
        }

        private static List<Variable> GetVariables(object packet)
        {
            var variables = new List<Variable>();

            foreach (PropertyInfo property in packet.GetType().GetProperties())
            {
                object[] attributes = property.GetCustomAttributes(typeof(VariableAttribute), false);

                if (attributes.Length != 1)
                {
                    continue;
                }

                variables.Add(new Variable(property, (VariableAttribute)attributes[0]));
            }

            foreach (FieldInfo field in packet.GetType().GetFields())
            {
                object[] attributes = field.GetCustomAttributes(typeof(VariableAttribute), false);

                if (attributes.Length != 1)
                {
                    continue;
                }

                variables.Add(new Variable(field, (VariableAttribute)attributes[0]));
            }

            return variables;
        }

        public static async Task SerializeAsync(Packet packet, MinecraftStream outStream)
        {
            List<Variable> variables = GetVariables(packet);

            using var stream = new MinecraftStream();
            foreach (Variable variable in variables.OrderBy(x => x.Attribute.Order))
            {
                object value = variable.GetValue(packet);

                try
                {
                    if (packet is EncryptionRequest && variable.Type == VariableType.String)
                    {
                        await stream.WriteStringAsync(string.Empty);
                        continue;
                    }

                    await WriteAsync(stream, variable, value);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + ": " + e.StackTrace);
                }
            }

            var data = stream.ToArray();

            int packetLength = data.Length + packet.PacketId.GetVarintLength();

            await outStream.WriteVarIntAsync(packetLength);
            await outStream.WriteVarIntAsync(packet.PacketId);
            await outStream.WriteAsync(data);
        }

        public static async Task<T> DeserializeAsync<T>(Packet packet) where T : new()
        {
            T newPacket = new T();

            List<Variable> variables = GetVariables(newPacket);

            using var stream = new MinecraftStream(packet.PacketData);

            foreach (Variable variable in variables)
            {
                var value = await ReadAsync(stream, variable);

                variable.SetValue(newPacket, value);
            }

            return newPacket;
        }

        public static async Task<Packet> ReadFromStreamAsync(MinecraftStream stream)
        {
            int length = await stream.ReadVarIntAsync();
            byte[] receivedData = new byte[length];

            await stream.ReadAsync(receivedData, 0, length);

            int packetId = 0;
            byte[] packetData = Array.Empty<byte>();

            using var packetStream = new MinecraftStream(receivedData);

            try
            {
                packetId = await packetStream.ReadVarIntAsync();
                int arlen = 0;

                if (length - packetId.GetVarintLength() > -1)
                    arlen = length - packetId.GetVarintLength();

                packetData = new byte[arlen];
                await packetStream.ReadAsync(packetData, 0, packetData.Length);
            }
            catch
            {
                throw;
            }

            return new EmptyPacket(packetId, packetData);
        }
    }
}