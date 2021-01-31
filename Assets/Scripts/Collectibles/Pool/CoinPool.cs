using Collectibles.Controllers;

namespace Collectibles.Pool
{
    public class CoinPool : GameObjectPool<CoinCollectibleController>
    {
        public CoinPool(GameObjectPoolData data) : base(data)
        {
        }
    }

    public class ChestPool : GameObjectPool<ChestCollectibleController>
    {
        public ChestPool(GameObjectPoolData data) : base(data)
        {
        }
    }
}