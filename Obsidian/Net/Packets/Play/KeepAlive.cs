using Obsidian.Util;

namespace Obsidian.Net.Packets
{
    public class KeepAlive : Packet
    {
        public KeepAlive(long id) : base(0x21, System.Array.Empty<byte>())
        {
            this.KeepAliveId = id;
        }

        public KeepAlive(byte[] data) : base(0x21, data)
        {
        }

        [Variable(0)]
        public long KeepAliveId { get; set; }
    }
}