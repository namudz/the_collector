using InterfaceAdapters.Game.Level;

namespace PresentationLayer.Game.Collectibles
{
    public class CoinCollectible : CollectibleController
    {
        private CoinDto _coin;

        protected override void GetDependencies()
        {
            base.GetDependencies();
            _coin = _collectibleConfig.Collectible as CoinDto;
        }

        protected override int GetScore()
        {
            return _coin.Score;
        }
    }
}