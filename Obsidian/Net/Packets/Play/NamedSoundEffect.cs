using Obsidian.Entities;
using Obsidian.Util;
using System;
using System.Threading.Tasks;

namespace Obsidian.Net.Packets
{
    class NamedSoundEffect : Packet
    {
        public NamedSoundEffect(string name, Position location, SoundCategory category, float pitch, float volume) : base(0x1A)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            this.Name = name;
            this.Category = category;
            this.Location = location;
            this.Volume = volume;
            this.Pitch = pitch;
        }

        [Variable(0)]
        public string Name { get; }

        [Variable(1)]
        public SoundCategory Category { get; }

        [Variable(2)]
        public Position Location { get; }

        [Variable(3)]
        public float Volume { get; }

        [Variable(4)]
        public float Pitch { get; }

        public override async Task<byte[]> SerializeAsync()
        {
            using (var stream = new MinecraftStream())
            {
                await stream.WriteStringAsync(this.Name);
                await stream.WriteVarIntAsync(this.Category);
                await stream.WriteIntAsync((int)this.Location.X * 8);
                await stream.WriteIntAsync((int)this.Location.Y * 8);
                await stream.WriteIntAsync((int)this.Location.Z * 8);
                await stream.WriteFloatAsync(this.Volume);
                await stream.WriteFloatAsync(this.Pitch);
                return stream.ToArray();
            }
        }
    }
}
