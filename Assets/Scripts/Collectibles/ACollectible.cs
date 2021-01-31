namespace Collectibles
{
    public abstract class ACollectible : ICollectible
    {
        private readonly Collectible _collectible;

        public float GetRespawnTime()
        {
            throw new System.NotImplementedException();
        }

        public Collectible Collect()
        {
            throw new System.NotImplementedException();
        }

        public void Spawn()
        {
            throw new System.NotImplementedException();
        }
    }

    public class CoinCollectible : ACollectible
    {
    }

    public class ChestCollectible : ACollectible
    {
        
    }
    
}