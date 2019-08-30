using Obsidian.Entities;
using Obsidian.Util;

namespace Obsidian.Net.Packets
{
    public class SoundEffect : Packet
    {
        public SoundEffect(int soundId, Position location, SoundCategory category = SoundCategory.Master, float pitch = 1.0f, float volume = 1f) : base(0x4D, System.Array.Empty<byte>())
        {
            this.SoundId = soundId;
            this.X = (int)location.X;
            this.Y = (int)location.Y;
            this.Z = (int)location.Z;
            this.Category = category;
            this.Pitch = pitch;
            this.Volume = volume;
        }

        [Variable(0)]
        public int SoundId { get; set; }

        [Variable(1)]
        public SoundCategory Category { get; set; }

        [Variable(2)]
        public int X { get; set; }

        [Variable(3)]
        public int Y { get; set; }

        [Variable(4)]
        public int Z { get; set; }

        [Variable(5)]
        public float Volume { get; set; }

        [Variable(6)]
        public float Pitch { get; set; }


        public Position Location => new Position
        {
            X = this.X,

            Y = this.Y,

            Z = this.Z
        };
    }
}