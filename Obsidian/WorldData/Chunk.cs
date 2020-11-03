using Obsidian.Blocks;
using Obsidian.ChunkData;
using Obsidian.Nbt.Tags;
using Obsidian.API;
using Obsidian.Util.Registry;
using System;
using System.Collections.Generic;

namespace Obsidian.WorldData
{
    public class Chunk
    {
        public int X { get; }
        public int Z { get; }

        public BiomeContainer BiomeContainer { get; private set; } = new BiomeContainer();

        public Dictionary<short, short> Blocks { get; private set; } = new Dictionary<short, short>();

        public ChunkSection[] Sections { get; private set; } = new ChunkSection[16];
        public List<NbtTag> BlockEntities { get; private set; } = new List<NbtTag>();

        public Dictionary<HeightmapType, Heightmap> Heightmaps { get; private set; } = new Dictionary<HeightmapType, Heightmap>();

        public Chunk(int x, int z)
        {
            this.X = x;
            this.Z = z;

            this.Heightmaps.Add(HeightmapType.MotionBlocking, new Heightmap(HeightmapType.MotionBlocking, this));

            this.Init();
        }

        private void Init()
        {
            for (int i = 0; i < 16; i++)
                this.Sections[i] = new ChunkSection();
        }

        public Block GetBlock(Position position) => this.GetBlock((int)position.X, (int)position.Y, (int)position.Z);

        public Block GetBlock(int x, int y, int z)
        {
            var value = (short)((x << 8) | (z << 4) | y);
            return Registry.GetBlock(this.Blocks.GetValueOrDefault(value)) ?? this.Sections[y >> 4].GetBlock(x, y, z) ?? Registry.GetBlock(Materials.Air);
        }

        public void SetBlock(Position position, Block block) => this.SetBlock((int)position.X, (int)position.Y, (int)position.Z, block);

        public void SetBlock(int x, int y, int z, Block block)
        {
            var value = (short)((x << 8) | (z << 4) | y);

            this.Blocks[value] = (short)block.Id;

            this.Sections[y >> 4].SetBlock(x, y & 15, z, block);

           
        }
    }
}