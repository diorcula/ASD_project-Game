using System;
using Display;

namespace WorldGeneration
{
    public class MapFactory
    {
        public static Map GenerateMap(string dbLocation = "C:\\Temp\\ChunkDatabase.db", String collectionName = "ChunkMap", int chunkSize = 8, int seed = -1123581321)
        {
            // default chunksize is 8. Can be adjusted in the line above
            
            // seed can be null, if it is it becomes random. But because of how c# works you can't set a default null, so this workaround exists.
            if (seed == -1123581321)
            {
                seed = new Random().Next(1, 999999);
            }
            
            return new Map(new NoiseMapGenerator(), new DatabaseFunctions.Database(dbLocation, collectionName), chunkSize, seed, new Display.ConsolePrinter());
        }

        public static int GenerateSeed()
        {
            var randomSeed = new Random().Next(1, 999999);
            return randomSeed;
        }
    }
}