using Collectibles.Config;
using EventDispatcher;
using UnityEngine;

namespace Collectibles.Controllers
{
    public class ChestCollectible : CollectibleController
    {
        [Header("Custom Components")]
        [SerializeField] private Animator _animator;
        
        private static readonly int Collected = Animator.StringToHash("Collected");
        private IGameScoreboard _gameScoreboard;
        private Chest _chestConfig;

        protected override void GetDependencies()
        {
            base.GetDependencies();
            _gameScoreboard = ServiceLocator.Instance.GetService<IGameScoreboard>();
            _chestConfig = _collectibleConfig.Collectible as Chest;
        }

        protected override void UpdateViewOnCollect()
        {
            _animator.SetTrigger(Collected);
        }

        protected override void DefaultCollect()
        {
            // Nothing, as the hiding will be triggered by the animation
        }

        protected override int GetScore()
        {
            return Mathf.RoundToInt(_gameScoreboard.CurrentScore * (_chestConfig.AdditionalScorePercentage / 100f));
        }

        protected override void Reset(ISignal signal)
        {
            _animator.Rebind();
            base.Reset(signal);
        }
    }
}