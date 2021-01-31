using Collectibles.Config;
using EventDispatcher;
using Presentation.Game.Collectibles;
using UnityEngine;

namespace Collectibles.Controllers
{
    public class ChestCollectible : CollectibleController
    {
        [Header("Custom Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private ChestCountdownView _countdownView;
        
        private static readonly int Collected = Animator.StringToHash("Collected");
        private IGameScoreboard _gameScoreboard;
        private Chest _chestConfig;

        protected override void GetDependencies()
        {
            base.GetDependencies();
            _gameScoreboard = ServiceLocator.Instance.GetService<IGameScoreboard>();
            _chestConfig = _collectibleConfig.Collectible as Chest;
        }

        public override void HandleSpawn()
        {
            base.HandleSpawn();
            _countdownView.StartCountdown(_chestConfig.ExpirationTime);
            Invoke(nameof(HideAndRespawn), _chestConfig.ExpirationTime);
        }

        protected override void UpdateViewOnCollect()
        {
            _animator.SetTrigger(Collected);
        }

        protected override void DefaultCollect()
        {
            // Nothing else, as the hiding will be triggered by the animation
            _countdownView.Hide();
        }

        protected override int GetScore()
        {
            if (_gameScoreboard.CurrentScore == 0)
            {
                return _chestConfig.MinScore;
            }
            
            return Mathf.RoundToInt(_gameScoreboard.CurrentScore * (_chestConfig.AdditionalScorePercentage / 100f));
        }

        protected override void Reset(ISignal signal)
        {
            _animator.Rebind();
            _countdownView.StopCountdown();
            base.Reset(signal);
        }

        protected override void HandleGameOver(ISignal signal)
        {
            _countdownView.StopCountdown();
            base.HandleGameOver(signal);
        }
    }
}