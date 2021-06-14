﻿using ASD_Game.ActionHandling;

namespace ASD_Game.Items.Services
{
    public interface IItemService
    {
        public int ChanceThereIsAItem { get; set; }
        public Item GenerateItemFromNoise(float noiseResult, int x, int y);
        public ISpawnHandler GetSpawnHandler();
    }
}