using Obsidian.Util;

namespace Obsidian.Net.Packets
{
    public class IncomingChatMessage : Packet
    {
        public IncomingChatMessage(byte[] data) : base(0x02, data)
        {
        }

        [Variable(0)]
        public string Message { get; private set; }
    }
}