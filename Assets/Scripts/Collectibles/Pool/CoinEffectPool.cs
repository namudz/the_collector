using EventDispatcher;
using Presentation.Game;

namespace Collectibles.Pool
{
    public class CoinEffectPool : GameObjectPool<CoinEffectView>
    {
        public CoinEffectPool(GameObjectPoolData data) : base(data, ServiceLocator.Instance.GetService<IEventDispatcher>())
        {
        }
    }
}