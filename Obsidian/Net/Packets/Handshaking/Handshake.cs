using Obsidian.Util;

namespace Obsidian.Net.Packets
{
    public class Handshake : Packet
    {
        [Variable]
        public ProtocolVersion Version { get; set; }

        [Variable]
        public string ServerAddress { get; set; }

        [Variable]
        public ushort ServerPort { get; set; }

        [Variable]
        public ClientState NextState { get; set; }

        public Handshake(byte[] data) : base(0x00, data)
        {
        }

        public Handshake() : base(0x00, null)
        {
        }
    }
}