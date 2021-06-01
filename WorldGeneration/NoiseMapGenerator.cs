﻿using WorldGeneration.Models;
using WorldGeneration.Models.HazardousTiles;
using WorldGeneration.Models.Interfaces;
using WorldGeneration.Models.TerrainTiles;

namespace WorldGeneration
{
    public class NoiseMapGenerator : INoiseMapGenerator
    {
        private IFastNoise _noise;

        public NoiseMapGenerator()
        {
            _noise = new FastNoiseLite();
            _noise.SetNoiseType(FastNoiseLite.NoiseType.Cellular);
            _noise.SetFrequency(0.015f);
            _noise.SetCellularReturnType(FastNoiseLite.CellularReturnType.CellValue);
        }

        public int[,] GenerateAreaMap(int size, int seed)
        {
            _noise.SetSeed(seed);
            var noiseData = new int[size, size];
            for (var y = 0; y < size; y++)
            for (var x = 0; x < size; x++)
                noiseData[x, y] = (int) _noise.GetNoise(x, y);
            return noiseData;
        }

        public Chunk GenerateChunk(int chunkX, int chunkY, int chunkRowSize, int seed)
        {
            _noise.SetSeed(seed);
            var map = new ITile[chunkRowSize * chunkRowSize];
            for (var y = 0; y < chunkRowSize; y++)
            {
                for (var x = 0; x < chunkRowSize; x++)
                {
                    map[y * chunkRowSize + x] = GetTileFromNoise(_noise.GetNoise(x + chunkX * chunkRowSize, y + chunkY * chunkRowSize)
                        , x + chunkRowSize * chunkX
                        , chunkRowSize * chunkY - chunkRowSize + y);
                }
            }
            return new Chunk(chunkX, chunkY, map, chunkRowSize);
        }

        public ITile GetTileFromNoise(float noise, int x, int y)
        {
            // this function is public for unit testing purposes only.
            return (noise * 10) switch
            {
                (< -8) => new WaterTile(x, y),
                (< -4) => new DirtTile(x, y),
                (< 2) => new GrassTile(x, y),
                (< 3) => new SpikeTile(x, y),
                (< 8) => new StreetTile(x, y),
                _ => new GasTile(x, y)
            };
        }

        public void SetNoise(IFastNoise noise)
        {
            // This function exists for unit testing purposes.
            _noise = noise;
        }
    }
}