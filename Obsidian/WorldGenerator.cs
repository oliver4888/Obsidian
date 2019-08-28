using System.Collections.Generic;

namespace Obsidian
{
    public abstract class WorldGenerator
    {
        public List<Chunk> Chunks { get; }

        public string Id { get; }

        public WorldGenerator(string id)
        {
            this.Chunks = new List<Chunk>();
            this.Id = id;
        }

        public abstract Chunk GenerateChunk(Chunk chunk);
    }
}