using Presentation.Game;
using Services;
using Services.EventDispatcher;
using Services.Pooling;

namespace Collectibles.Pool
{
    public class CoinEffectPool : GameObjectPool<CoinEffectView>
    {
        public CoinEffectPool(GameObjectPoolData data) : base(data, ServiceLocator.Instance.GetService<IEventDispatcher>())
        {
        }
    }
}