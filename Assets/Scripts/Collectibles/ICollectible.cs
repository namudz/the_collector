namespace Collectibles
{
    public interface ICollectible
    {
        Collectible.CollectibleType Type { get; }
        int Collect();
    }
}