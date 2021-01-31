namespace Collectibles.Pool
{
    public class CoinPool : GameObjectPool<CoinCollectible>
    {
        public CoinPool(GameObjectPoolData data) : base(data)
        {
        }
    }

    public class ChestPool : GameObjectPool<ChestCollectible>
    {
        public ChestPool(GameObjectPoolData data) : base(data)
        {
        }
    }
}