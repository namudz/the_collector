using System;

namespace Collectibles.Config
{
    [Serializable]
    public class Collectible
    {
        public enum CollectibleType
        {
            Coin,
            Chest
        }

        public CollectibleType Type;
        public RespawnCooldown RespawnTime;
        public int SpawnWeight;

        [Serializable]
        public class RespawnCooldown
        {
            public float MinTime;
            public float MaxTime;
        }
    }
}