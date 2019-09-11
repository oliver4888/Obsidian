using Obsidian.Util;
using System;

namespace Obsidian.Net.Packets
{
    public class PlayerDigging : Packet
    {
        [Variable(0)]
        public int Status { get; private set; }

        [Variable(1)]
        public Position Location { get; private set; }

        [Variable(2)]
        public sbyte Face { get; private set; } // This is an enum of what face of the block is being hit

        public PlayerDigging() : base(0x18, Array.Empty<byte>()) { }
        public PlayerDigging(byte[] packetdata) : base(0x18, packetdata) { }
    }
}
