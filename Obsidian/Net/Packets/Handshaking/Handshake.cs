using Obsidian.Util;

namespace Obsidian.Net.Packets
{
    public class Handshake : Packet
    {
        [Variable(VariableType.VarInt)]
        public ProtocolVersion Version { get; set; }

        [Variable(VariableType.String)]
        public string ServerAddress { get; set; }

        [Variable(VariableType.UnsignedShort)]
        public ushort ServerPort { get; set; }

        [Variable(VariableType.VarInt)]
        public ClientState NextState { get; set; }

        public Handshake(byte[] data) : base(0x00, data)
        {
        }

        public Handshake() : base(0x00, null)
        {
        }
    }
}