namespace Collectibles
{
    public interface ICollectible
    {
        float GetRespawnTime();
        Collectible Collect();
        void Spawn();
    }
}