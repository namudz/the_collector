using System;

namespace Collectibles
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
        public int Score;
        public RespawnCooldown RespawnTime;
        public float ExpirationTime;
        public int SpawnWeight;

        [Serializable]
        public class RespawnCooldown
        {
            public float MinTime;
            public float MaxTime;
        }
    }
}