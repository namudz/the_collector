using DomainLayer.Collectibles;
using Game;
using Presentation.Game.Collectibles;
using Services;
using Services.EventDispatcher;
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
        private IGame _iGame;

        protected override void GetDependencies()
        {
            base.GetDependencies();
            _iGame = ServiceLocator.Instance.GetService<IGame>();
            _gameScoreboard = ServiceLocator.Instance.GetService<IGameScoreboard>();
            _chestConfig = _collectibleConfig.Collectible as Chest;
        }

        public override void HandleSpawn()
        {
            base.HandleSpawn();
            if (_iGame.HasGameStarted)
            {
                StartCountdown();
            }
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
            var percentageScore =
                Mathf.RoundToInt(_gameScoreboard.CurrentScore * (_chestConfig.AdditionalScorePercentage / 100f));

            return percentageScore > _chestConfig.MinScore ? percentageScore : _chestConfig.MinScore;
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
        
        protected override void HandleGameStarted(ISignal signal)
        {
            StartCountdown();
            base.HandleGameStarted(signal);
        }

        private void StartCountdown()
        {
            if (!_gameObject.activeSelf) { return; }
            _countdownView.StartCountdown(_chestConfig.ExpirationTime);
            Invoke(nameof(HideAndRespawn), _chestConfig.ExpirationTime);
        }
    }
}