using System;
using Collectibles.Config;
using Collectibles.Pool;
using Game.Signals;
using Presentation.Game;
using Services;
using Services.EventDispatcher;
using Services.Pooling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Collectibles.Controllers
{
    public abstract class CollectibleController : MonoBehaviour, ICollectible, IPoolable
    {
        public event Action<CollectibleController> OnSpawnPointIsFree;

        public Collectible.CollectibleType Type => _collectibleConfig.Collectible.Type;
        
        [Header("Base Components")]
        [SerializeField] protected CollectibleConfig _collectibleConfig;
        [SerializeField] private BoxCollider2D _collider2D;

        protected GameObject _gameObject;
        protected IEventDispatcher _eventDispatcher;
        private IGameObjectPool<CoinEffectView> _coinEffectPool;

        private void Awake()
        {
            _gameObject = gameObject;
            GetDependencies();
        }

        protected virtual void GetDependencies()
        {
            _coinEffectPool = ServiceLocator.Instance.GetService<IGameObjectPool<CoinEffectView>>();
            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            _eventDispatcher.Subscribe<GameOverSignal>(HandleGameOver);
            _eventDispatcher.Subscribe<GameResetSignal>(Reset);
            _eventDispatcher.Subscribe<GameStartedSignal>(HandleGameStarted);
        }

        private void OnDestroy()
        {
            OnSpawnPointIsFree = null;
            _eventDispatcher.Unsubscribe<GameOverSignal>(HandleGameOver);
            _eventDispatcher.Unsubscribe<GameResetSignal>(Reset);
            _eventDispatcher.Unsubscribe<GameStartedSignal>(HandleGameStarted);
            CancelInvoke();
        }

        public virtual void HandleSpawn()
        {
            _collider2D.enabled = true;
        }

        public int Collect()
        {
            _collider2D.enabled = false;
            ShowCoinEffect();
            UpdateViewOnCollect();
            DefaultCollect();
            
            return GetScore();
        }

        private void ShowCoinEffect()
        {
            var effectView = _coinEffectPool.GetInstance(transform.position);
            effectView.GetComponent<CoinEffectView>()?.Show(GetScore());
        }

        protected abstract int GetScore();

        protected virtual void UpdateViewOnCollect()
        {
        }

        protected virtual void DefaultCollect()
        {
            HideAndRespawn();
        }

        protected virtual void HandleGameOver(ISignal signal)
        {
            CancelInvoke();
        }

        protected virtual void HandleGameStarted(ISignal signal)
        {
            
        }

        protected virtual void Reset(ISignal signal)
        {
            if (_gameObject.activeSelf)
            {
                _gameObject.SetActive(false);
                BackToPool();
            }
            OnSpawnPointIsFree = null;
            CancelInvoke();
        }

        protected void HideAndRespawn()
        {
            _gameObject.SetActive(false);
            BackToPool();

            var respawnTime = GetRespawnTime();
            Invoke(nameof(CanBeRespawned), respawnTime);
        }
        
        protected abstract void BackToPool();

        private float GetRespawnTime()
        {
            var respawnInfo = _collectibleConfig.Collectible.RespawnTime;
            return Random.Range(respawnInfo.MinTime, respawnInfo.MaxTime);
        }
        
        private void CanBeRespawned()
        {
            OnSpawnPointIsFree?.Invoke(this);
        }
    }
}