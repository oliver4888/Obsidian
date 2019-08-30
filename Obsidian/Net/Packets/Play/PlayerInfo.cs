using Obsidian.PlayerData.Info;
using Obsidian.Util;
using System.Collections.Generic;

namespace Obsidian.Net.Packets
{
    public class PlayerInfo : Packet
    {
        [Variable(0)]
        public int Action { get; }

        [Variable(1)]
        public int ActionCount => Actions.Count;

        [Variable(2)]
        public List<PlayerInfoAction> Actions { get; }

        public PlayerInfo(int action, List<PlayerInfoAction> actions) : base(0x30)
        {
            this.Action = action;
            this.Actions = actions;
        }
    }
}