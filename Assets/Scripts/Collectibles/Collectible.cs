using System;

namespace Collectibles
{
    [Serializable]
    public class Collectible
    {
        public int Score;
        public RespawnCooldown RespawnTime;
        // TODO - Spawn items based on weight
        public int SpawnWeight;

        [Serializable]
        public class RespawnCooldown
        {
            public float MinTime;
            public float MaxTime;
        }
    }
}