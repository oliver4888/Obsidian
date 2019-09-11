using Obsidian.Util;
using System;

namespace Obsidian.Net.Packets
{
    public class PlayerPosition : Packet
    {
        public PlayerPosition(Position pos, bool onground) : base(0x10, System.Array.Empty<byte>())
        {
            this.X = pos.X;
            this.Y = pos.Y;
            this.Z = pos.Z;
            this.OnGround = onground;
        }

        public PlayerPosition() : base(0x10, Array.Empty<byte>()) { }
        public PlayerPosition(byte[] data) : base(0x10, data) { }

        [Variable(0)]
        public double X { get; set; }

        [Variable(1)]
        public double Y { get; set; }

        [Variable(2)]
        public double Z { get; set; }

        [Variable(3)]
        public bool OnGround { get; private set; } = false;

        public Position Position => new Position
        {
            X = this.X,

            Y = this.Y,

            Z = this.Z
        };
    }
}