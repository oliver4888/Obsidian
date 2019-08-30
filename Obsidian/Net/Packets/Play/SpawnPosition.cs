using Obsidian.Util;

namespace Obsidian.Net.Packets
{
    public class SpawnPosition : Packet
    {
        public SpawnPosition(Position location) : base(0x49, System.Array.Empty<byte>()) => Location = location;

        [Variable(0)]
        public Position Location { get; private set; }
    }
}