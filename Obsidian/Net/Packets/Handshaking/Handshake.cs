using Obsidian.Util;

namespace Obsidian.Net.Packets
{
    public class Handshake : Packet
    {
        [Variable(0, VariableType.VarInt)]
        public ProtocolVersion Version { get; set; }

        [Variable(1, VariableType.String)]
        public string ServerAddress { get; set; }

        [Variable(2, VariableType.UnsignedShort)]
        public ushort ServerPort { get; set; }

        [Variable(3, VariableType.VarInt)]
        public ClientState NextState { get; set; }

        public Handshake(byte[] data) : base(0x00, data)
        {
        }

        public Handshake() : base(0x00, null)
        {
        }
    }
}