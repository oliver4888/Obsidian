using Obsidian.Util;
using Obsidian.Util.DataTypes;

namespace Obsidian.Net.Packets
{
    public class PlayerBlockPlacement : Packet
    {
        [Variable(0)]
        public Position Location { get; private set; }

        [Variable(1)]
        public BlockFace Face { get; private set; } // enum with face

        [Variable(2)]
        public int Hand { get; private set; } // hand it was placed from. 0 is main, 1 is off

        [Variable(3)]
        public float CursorX { get; private set; }

        [Variable(4)]
        public float CursorY { get; private set; }

        [Variable(5)]
        public float CursorZ { get; private set; }

        public PlayerBlockPlacement(Position loc, BlockFace face, int hand, float cursorx, float cursory, float cursorz) : base(0x29, System.Array.Empty<byte>())
        {
            Location = loc;
            Face = face;
            Hand = hand;
            CursorX = cursorx;
            CursorY = cursory;
            CursorZ = cursorz;
        }

        public PlayerBlockPlacement(byte[] data) : base(0x29, data) { }
    }
}