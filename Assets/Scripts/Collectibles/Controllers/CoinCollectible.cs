using Collectibles.Config;
using Collectibles.Pool;

namespace Collectibles.Controllers
{
    public class CoinCollectible : CollectibleController
    {
        private Coin _coin;
        private IGameObjectPool<CoinCollectible> _pool;

        protected override void GetDependencies()
        {
            base.GetDependencies();
            _coin = _collectibleConfig.Collectible as Coin;
            _pool = ServiceLocator.Instance.GetService<IGameObjectPool<CoinCollectible>>();
        }

        protected override int GetScore()
        {
            return _coin.Score;
        }

        protected override void BackToPool()
        {
            _pool.BackToPool(_gameObject);
        }
    }
}