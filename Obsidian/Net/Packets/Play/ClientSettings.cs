using Obsidian.Util;
using System;

namespace Obsidian.Net.Packets
{
    public class ClientSettings : Packet
    {
        public ClientSettings() : base(0x04, Array.Empty<byte>()) { }
        public ClientSettings(byte[] data) : base(0x04, data) { }

        [Variable(0)]
        public string Locale { get; private set; }

        [Variable(1)]
        public sbyte ViewDistance { get; private set; }

        [Variable(2, VariableType.Int)]
        public int ChatMode { get; private set; }

        [Variable(3)]
        public bool ChatColors { get; private set; }

        [Variable(4)]
        public byte SkinParts { get; private set; } // skin parts that are displayed. might not be necessary to decode?

        [Variable(5)]
        public int MainHand { get; private set; }
    }
}