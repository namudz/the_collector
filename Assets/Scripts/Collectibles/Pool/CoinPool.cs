using Collectibles.Controllers;
using EventDispatcher;

namespace Collectibles.Pool
{
    public class CoinPool : GameObjectPool<CoinCollectible>
    {
        public CoinPool(GameObjectPoolData data) : base(data, ServiceLocator.Instance.GetService<IEventDispatcher>())
        {
        }
    }
}