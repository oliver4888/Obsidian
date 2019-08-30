using Obsidian.Util;

namespace Obsidian.Net.Packets
{
    public class PlayerLook : Packet
    {
        /*public PlayerLook(float yaw, float pitch, bool onground)
        {
            this.Yaw = yaw;
            this.Pitch = pitch;
            this.OnGround = onground;
        }*/

        public PlayerLook(byte[] data) : base(0x00, data)
        {
        }

        [Variable(0)]
        public float Yaw { get; private set; } = 0;

        [Variable(1)]
        public float Pitch { get; private set; } = 0;

        [Variable(2)]
        public bool OnGround { get; private set; } = false;
    }
}