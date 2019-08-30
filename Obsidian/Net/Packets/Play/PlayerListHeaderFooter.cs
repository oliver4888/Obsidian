using Obsidian.Chat;
using Obsidian.Util;

namespace Obsidian.Net.Packets.Play
{
    public class PlayerListHeaderFooter : Packet
    {
        public PlayerListHeaderFooter(ChatMessage header, ChatMessage footer) : base(0x4E)
        {
            this.Header = header ?? new ChatMessage()
            {
                HoverEvent = new TextComponent { Translate = "" }
            };

            this.Footer = footer ?? new ChatMessage()
            {
                HoverEvent = new TextComponent { Translate = "" }
            };
        }

        [Variable(0)]
        public ChatMessage Header { get; }

        [Variable(1)]
        public ChatMessage Footer { get; }
    }
}