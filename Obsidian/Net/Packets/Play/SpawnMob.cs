using Obsidian.Entities;
using Obsidian.Util;
using System;

namespace Obsidian.Net.Packets.Play
{
    public class SpawnMob : Packet
    {
        public SpawnMob(int id, Guid uuid, int type, Transform transform, float headPitch, Velocity velocity, Entity entity) : base(0x03)
        {
            this.Id = id;
            this.Uuid = uuid;
            this.Type = type;
            this.Transform = transform ?? throw new ArgumentNullException(nameof(transform));
            this.HeadPitch = headPitch;
            this.Velocity = velocity;
            this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        [Variable(0)]
        public int Id { get; }

        [Variable(1)]
        public Guid Uuid { get; }

        [Variable(2)]
        public int Type { get; }

        [Variable(3)]
        public Transform Transform { get; }

        [Variable(4)]
        public float HeadPitch { get; }

        [Variable(5)]
        public Velocity Velocity { get; }

        [Variable(6)]
        public Entity Entity { get; }
    }
}