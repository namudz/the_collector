using Collectibles.Controllers;
using Services;
using Services.EventDispatcher;
using Services.Pooling;

namespace Collectibles.Pool
{
    public class CoinPool : GameObjectPool<CoinCollectible>
    {
        public CoinPool(GameObjectPoolData data) : base(data, ServiceLocator.Instance.GetService<IEventDispatcher>())
        {
        }
    }
}