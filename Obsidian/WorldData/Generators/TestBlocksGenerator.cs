﻿using Obsidian.Blocks;
using Obsidian.Util.Registry;

namespace Obsidian.WorldData.Generators
{
    public class TestBlocksGenerator : WorldGenerator
    {
        public TestBlocksGenerator() : base("test") { }

        public override Chunk GenerateChunk(int x, int z)
        {
            var chunk = new Chunk(x, z);

            int countX = 0;
            int countZ = 0;

            foreach (var block in Registry.Blocks.Values)
            {
                if (block.IsAir || block is BlockBed)
                    continue;

                if (countX == 16)
                {
                    countX = 0;
                    countZ++;
                }

                chunk.SetBlock(countX, 1, countZ, block);

                countX++;
            }

            this.Chunks.Add(chunk);

            return chunk;
        }
    }
}
