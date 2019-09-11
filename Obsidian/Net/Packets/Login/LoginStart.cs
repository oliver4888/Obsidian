using Obsidian.Util;

namespace Obsidian.Net.Packets
{
    public class LoginStart : Packet
    {
        public LoginStart() : base(0x00, System.Array.Empty<byte>()) { }

        public LoginStart(byte[] data) : base(0x00, data) { }

        [Variable(0)]
        public string Username { get; private set; }
    }
}