using System;
using Collectibles.Config;
using Collectibles.Pool;
using EventDispatcher;
using Game.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Collectibles.Controllers
{
    public abstract class CollectibleController : MonoBehaviour, ICollectible
    {
        public event Action<CollectibleController> OnSpawnPointIsFree;
        public Collectible.CollectibleType Type => _collectibleConfig.Collectible.Type;
        
        [Header("Base Components")]
        [SerializeField] protected CollectibleConfig _collectibleConfig;
        [SerializeField] private BoxCollider2D _collider2D;

        private IGameObjectPool _objectPool;
        private GameObject _gameObject;
        private IEventDispatcher _eventDispatcher;

        private void Awake()
        {
            _gameObject = gameObject;
            GetDependencies();
        }

        protected virtual void GetDependencies()
        {
            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            _eventDispatcher.Subscribe<GameOverSignal>(HandleGameOver);
            _eventDispatcher.Subscribe<GameResetSignal>(Reset);
        }

        private void OnDestroy()
        {
            OnSpawnPointIsFree = null;
            _eventDispatcher.Unsubscribe<GameOverSignal>(HandleGameOver);
            _eventDispatcher.Unsubscribe<GameResetSignal>(Reset);
            CancelInvoke();
        }

        public void SetPool(IGameObjectPool objectPool)
        {
            _objectPool = objectPool;
        }

        public void HandleSpawn()
        {
            _collider2D.enabled = true;
        }

        public virtual int Collect()
        {
            _collider2D.enabled = false;
            UpdateViewOnCollect();
            DefaultCollect();
            
            return GetScore();
        }

        protected abstract int GetScore();

        protected virtual void UpdateViewOnCollect()
        {
        }

        protected virtual void DefaultCollect()
        {
            HideAndRespawn();
        }

        private void HandleGameOver(ISignal signal)
        {
            CancelInvoke();
        }

        protected virtual void Reset(ISignal signal)
        {
            _gameObject.SetActive(false);
            _objectPool.BackToPool(_gameObject);
            CancelInvoke();
        }

        private void HideAndRespawn()
        {
            _gameObject.SetActive(false);
            _objectPool.BackToPool(_gameObject);

            var respawnTime = GetRespawnTime();
            Invoke(nameof(CanBeRespawned), respawnTime);
        }

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