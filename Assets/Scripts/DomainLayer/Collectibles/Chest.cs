using System;

namespace DomainLayer.Collectibles
{
    [Serializable]
    public class Chest : Collectible
    {
        public float ExpirationTime;
        public float AdditionalScorePercentage;
        public int MinScore;
    }
}