using Collectibles.Config;

namespace Collectibles.Controllers
{
    public class CoinCollectible : CollectibleController
    {
        private Coin _coin;

        protected override void GetDependencies()
        {
            base.GetDependencies();
            _coin = _collectibleConfig.Collectible as Coin;
        }

        protected override int GetScore()
        {
            return _coin.Score;
        }
    }
}