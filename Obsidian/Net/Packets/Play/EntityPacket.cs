using Obsidian.Util;

namespace Obsidian.Net.Packets.Play
{
    public class EntityPacket : Packet
    {
        [Variable(0)]
        public int Id { get; set; }

        public EntityPacket() : base(0x27, System.Array.Empty<byte>()) { }
    }
}
