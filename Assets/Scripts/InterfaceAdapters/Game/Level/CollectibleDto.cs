using System;
using DomainLayer;
using UnityEngine;

namespace InterfaceAdapters.Game.Level
{
    public class CollectibleDto
    {
        public ItemType ItemType;
        public string Type => ItemType.ToString();
        
        public RespawnCooldown RespawnTime;
        [Min(0)] public int SpawnWeight;

        [Serializable]
        public class RespawnCooldown
        {
            public float MinTime;
            public float MaxTime;
        }
    }
    
    [Serializable]
    public class CoinDto : CollectibleDto
    {
        public int Score;
    }
    
    [Serializable]
    public class ChestDto : CollectibleDto
    {
        public float ExpirationTime;
        public float AdditionalScorePercentage;
        public int MinScore;
    }
}