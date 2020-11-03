using Obsidian.Nbt.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace Obsidian.Serializer.Region
{
    public class ChunkNbtMapper
    {
        public ChunkNbtMapper()
        {
            var blocks = new byte[4096];
            var data = new byte[2048];
            var skylight = new byte[2048];
            var blockLight = new byte[2048];
            byte y;
        }

        public NbtCompound ChunkStructure = new NbtCompound()
        {
            new NbtInt("DataVersion"),
            new NbtCompound("Level")
            {
                new NbtInt("xPos"),
                new NbtInt("zPos"),
                new NbtLong("LastUpdate"),
                new NbtLong("InhabitedTime"),
                new NbtIntArray("Biomes"),
                new NbtCompound("Heightmaps")
                {
                    new NbtLongArray("MOTION_BLOCKING"),
                    new NbtLongArray("MOTION_BLOCKING_NO_LEAVES"),
                    new NbtLongArray("OCEAN_FLOOR"),
                    new NbtLongArray("OCEAN_FLOOR_WG"),
                    new NbtLongArray("WORLD_SURFACE"),
                    new NbtLongArray("WORLD_SURFACE_WG")
                },
                new NbtCompound("CarvingMasks")
                {
                    new NbtByteArray("AIR"),
                    new NbtByteArray("LIQUID")
                },
                new NbtList("Sections",
                    new NbtCompound()
                    {
                        new NbtByte("Y"),
                        new NbtByteArray("Blocks"),
                        new NbtByteArray("Data"),
                        new NbtByteArray("SkyLight"),
                        new NbtByteArray("BlockLight"),
                    })
            }
        };
    }
}
