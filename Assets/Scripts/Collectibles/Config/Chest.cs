using System;

namespace Collectibles.Config
{
    [Serializable]
    public class Chest : Collectible
    {
        public float ExpirationTime;
        public float AdditionalScorePercentage;
        public int MinScore;
    }
}