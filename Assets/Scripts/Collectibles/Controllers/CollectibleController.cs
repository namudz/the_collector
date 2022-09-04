using Collectibles.Config;
using Game.Signals;
using InterfaceAdapters.Game;
using Services;
using Services.EventDispatcher;
using Services.GameObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Collectibles.Controllers
{
    public abstract class CollectibleController : MonoBehaviour, ICollectible
    {
        public ItemType Type => _collectibleConfig.Collectible.Type;
        
        [Header("Base Components")]
        [SerializeField] protected CollectibleConfig _collectibleConfig;
        [SerializeField] private BoxCollider2D _collider2D;
        [SerializeField] private PoolableGameObject _poolableController;

        protected GameObject _gameObject;
        protected IEventDispatcher _eventDispatcher;

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
            _eventDispatcher.Subscribe<GameStartedSignal>(HandleGameStarted);
        }

        private void OnDestroy()
        {
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
            var signal = new ShowCoinFxSignal(transform.position, GetScore());
            _eventDispatcher.Dispatch(signal);
        }

        protected abstract int GetScore();

        protected virtual void UpdateViewOnCollect()
        {
        }

        protected virtual void DefaultCollect()
        {
            HideAndRespawn();
        }

        protected virtual void HandleGameOver(ISignal _)
        {
            CancelInvoke();
        }

        protected virtual void HandleGameStarted(ISignal _)
        {
        }

        protected virtual void Reset(ISignal _)
        {
            if (_gameObject.activeSelf)
            {
                BackToPool();
            }
        }

        protected void HideAndRespawn()
        {
            BackToPool();

            var respawnTime = GetRespawnTime();

            var signal = new RespawnCollectibleSignal(respawnTime, transform.position);
            _eventDispatcher.Dispatch(signal);
        }

        private void BackToPool()
        {
            _poolableController.BackToPool();
        }

        private float GetRespawnTime()
        {
            var respawnInfo = _collectibleConfig.Collectible.RespawnTime;
            return Random.Range(respawnInfo.MinTime, respawnInfo.MaxTime);
        }
    }
}