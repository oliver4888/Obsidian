using Obsidian.PlayerData;
using Obsidian.Util;

namespace Obsidian.Net.Packets
{
    public class JoinGame : Packet
    {
        [Variable(0, VariableType.Int)]
        public int EntityId { get; private set; }

        [Variable(1, VariableType.UnsignedByte)]
        public Gamemode GameMode { get; private set; } = Gamemode.Survival;

        [Variable(2, VariableType.Int)]
        public Dimension Dimension { get; private set; } = Dimension.Overworld;

        [Variable(3, VariableType.UnsignedByte)]
        public Difficulty Difficulty { get; private set; } = Difficulty.Peaceful;

        [Variable(4)]
        public byte MaxPlayers { get; private set; } = 0; // Gets ignored by client

        [Variable(5)]
        public string LevelType { get; private set; } = "default";

        [Variable(6)]
        public bool ReducedDebugInfo { get; private set; } = false;

        public JoinGame(byte[] data) : base(0x25, data) { }

        public JoinGame(int entityid, Gamemode gamemode, Dimension dimension, Difficulty difficulty, string leveltype, bool debugging) : base(0x25, System.Array.Empty<byte>())
        {
            this.EntityId = entityid;
            this.GameMode = gamemode;
            this.Dimension = dimension;
            this.Difficulty = difficulty;
            this.LevelType = leveltype;
            this.ReducedDebugInfo = !debugging;
        }
    }
}