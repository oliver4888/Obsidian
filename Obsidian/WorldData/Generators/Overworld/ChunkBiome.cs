using Obsidian.ChunkData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Obsidian.WorldData.Generators.Overworld
{
    public enum Temp
    {
        hot,
        warm,
        cold,
        freezing
    }

    public enum Humidity
    {
        wet,
        neutral,
        dry
    }

    public static class ChunkBiome
    {
        public static Biomes GetBiome(int worldX, int worldZ, OverworldNoise noiseGen)
        {
            Temp t;
            double temperature = noiseGen.GetBiomeTemp(worldX, 0, worldZ);
            if (temperature > 0.8) { t = Temp.hot; }
            else if (temperature > 0.45) { t = Temp.warm; }
            else if (temperature > -0.2) { t = Temp.cold; }
            else { t = Temp.freezing; }

            Humidity h;
            double humidity = noiseGen.GetBiomeHumidity(worldX, 0, worldZ);
            if (humidity > 0.33) { h = Humidity.dry; }
            else if (humidity > -0.33) { h = Humidity.neutral; }
            else { h = Humidity.wet; }

            Biomes b = Biomes.Nether;
            // River
            if (noiseGen.isRiver(worldX, worldZ))
            {
                switch (t)
                {
                    case Temp.hot:
                    case Temp.warm:
                    case Temp.cold:
                        b = Biomes.River;
                        break;
                    default:
                        b = Biomes.FrozenRiver;
                        break;
                }
            }
            // Mountain
            else if (noiseGen.isMountain(worldX, worldZ))
            {
                switch (t)
                {
                    case Temp.hot:
                        if (h == Humidity.wet) { b = Biomes.Mountains; }
                        else if (h == Humidity.neutral) { b = Biomes.Mountains; }
                        else { b = Biomes.ErodedBadlands; }
                        break;
                    case Temp.warm:
                        if (h == Humidity.wet) { b = Biomes.TaigaMountains; }
                        else if (h == Humidity.neutral) { b = Biomes.Mountains; }
                        else { b = Biomes.WoodedBadlandsPlateau; }
                        break;
                    case Temp.cold:
                        if (h == Humidity.wet) { b = Biomes.SnowyTaigaMountains; }
                        else if (h == Humidity.neutral) { b = Biomes.WoodedMountains; }
                        else { b = Biomes.Mountains; }
                        break;
                    case Temp.freezing:
                        if (h == Humidity.wet) { b = Biomes.SnowyTaigaMountains; }
                        else if (h == Humidity.neutral) { b = Biomes.SnowyMountains; }
                        else { b = Biomes.GravellyMountains; }
                        break;
                }
            } 
            // Badlands/Foothills
            else if (noiseGen.isBadlands(worldX, worldZ))
            {
                switch (t)
                {
                    case Temp.hot:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                    case Temp.warm:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                    case Temp.cold:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                    case Temp.freezing:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                }

                if (temperature >0)
                {
                    if (humidity >0)
                        b = Biomes.SwampHills;
                    else
                        b = Biomes.WoodedBadlandsPlateau;
                }
                else // Cold Badlands
                {
                    if (humidity >0)
                        b = Biomes.SnowyTundra;
                    else
                        b = Biomes.Badlands;
                }
            }
            // Hills
            else if (noiseGen.isHills(worldX, worldZ))
            {
                switch (t)
                {
                    case Temp.hot:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                    case Temp.warm:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                    case Temp.cold:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                    case Temp.freezing:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                }

                if (temperature >0)
                {
                    if (humidity >0)
                        b = Biomes.JungleHills;
                    else
                        b = Biomes.DesertHills;
                }
                else // Cold Hills
                {
                    if (humidity >0)
                        b = Biomes.BirchForestHills;
                    else
                        b = Biomes.DarkForestHills;
                }
            }
            //Plains
            else if (noiseGen.isPlains(worldX, worldZ))
            {
                switch (t)
                {
                    case Temp.hot:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                    case Temp.warm:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                    case Temp.cold:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                    case Temp.freezing:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                }

                if (temperature >0)
                {
                    if (humidity >0)
                        b = Biomes.Savanna;
                    else
                        b = Biomes.Desert;
                } 
                else // Cold Plains
                {
                    if (humidity >0)
                        b = Biomes.SnowyTaiga;
                    else
                        b = Biomes.Plains;
                }
            }
            // Ocean
/*            
            else if (noiseGen.isOcean(worldX, worldZ))
            {
                switch (t)
                {
                    case Temp.hot:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                    case Temp.warm:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                    case Temp.cold:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                    case Temp.freezing:
                        if (h == Humidity.wet) { }
                        else if (h == Humidity.neutral) { }
                        else { }
                        break;
                }

                bool isDeep = noiseGen.isDeepOcean(worldX, worldZ);
                if (temperature >0) // Hot Ocean
                {
                    if (humidity >0)
                    {
                        if (isDeep)
                            b = Biomes.DeepWarmOcean;
                        else
                            b = Biomes.WarmOcean;
                    }
                    else
                    {
                        if (isDeep)
                            b = Biomes.DeepLukewarmOcean;
                        else
                            b = Biomes.LukewarmOcean;
                    }
                }
                else // Cold Ocean
                {
                    if (humidity >0)
                    {
                        if (isDeep)
                            b = Biomes.DeepColdOcean;
                        else
                            b = Biomes.ColdOcean;
                    }
                    else
                    {
                        if (isDeep)
                            b = Biomes.DeepFrozenOcean;
                        else
                            b = Biomes.FrozenOcean;
                    }
                }
            }
            */
            else { b = Biomes.Plains; }
            return b;
        }
    }
}
