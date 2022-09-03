using System;
using UnityEngine;

namespace DomainLayer.Collectibles
{
    [Serializable]
    public class Collectible : Item
    {
        public RespawnCooldown RespawnTime;
        [Min(0)] public int SpawnWeight;

        [Serializable]
        public class RespawnCooldown
        {
            public float MinTime;
            public float MaxTime;
        }
    }
}