using Newtonsoft.Json;
using Obsidian.Net;
using Obsidian.Net.Packets;
using Obsidian.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Obsidian.Tests
{
    public class PacketTests
    {
        [MemberData(nameof(PacketData))]
        [Theory(DisplayName = "Compare packet")]
        public Task ComparePacketAsync(Packet packet) => CompareAsync(packet, GetPacketData(packet));

        private async Task CompareAsync(Packet packet, byte[] expected)
        {
            using var stream = new MinecraftStream();

            await PacketSerializer.SerializeAsync(packet, stream);

            byte[] actual = stream.ToArray();

            for (int i = 0; i < actual.Length; i++)
            {
                if (actual[i] != expected[i])
                {
                    throw new Exception($"Byte mismatch at index {i}, expected 0x{expected[i].ToString("X")} got 0x{actual[i].ToString("X")}");
                }
            }
        }

        private byte[] GetPacketData<T>(T packet) where T : Packet
        {
            string packetName = packet.GetType().FullName;
            string path = Path.Combine(@"A:\Code\dotnet\Obsidian\Obsidian\bin\Debug\netcoreapp2.1\export", packetName + ".bin");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Couldn't find file required for packet {packetName}");
            }

            return File.ReadAllBytes(path);
        }

        public static IEnumerable<object[]> PacketData => getPackets();

        public static IEnumerable<object[]> getPackets()
        {
            var packets = new List<object[]>();

            foreach (var filePath in Directory.GetFiles(@"A:\Code\dotnet\Obsidian\Obsidian\bin\Debug\netcoreapp2.1\export", "*.json"))
            {
                try
                {
                    var name = Path.GetFileNameWithoutExtension(filePath);

                    var type = Type.GetType(Assembly.CreateQualifiedName("Obsidian", name));

                    if (type == null)
                    {
                        var namespaces = name.Split('.').ToList();

                        namespaces.RemoveAt(3);

                        name = string.Join('.', namespaces);

                        type = Type.GetType(Assembly.CreateQualifiedName("Obsidian", name));
                    }

                    var json = File.ReadAllText(filePath);
                    var packet = JsonConvert.DeserializeObject(json, type);
                    packets.Add(new object[] { Convert.ChangeType(packet, type) });
                }
                catch (Exception)
                {
                }
            }

            return packets;
        }
    }
}