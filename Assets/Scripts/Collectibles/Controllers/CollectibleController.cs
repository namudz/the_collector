using System;
using Collectibles.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Collectibles.Controllers
{
    public abstract class CollectibleController : MonoBehaviour, ICollectible
    {
        public event Action<CollectibleController> OnSpawnPointIsFree;
        
        public Collectible.CollectibleType Type => _collectibleConfig.Collectible.Type;
        
        [SerializeField] protected CollectibleConfig _collectibleConfig;

        private IGameObjectPool _objectPool;
        private GameObject _gameObject;

        private void Awake()
        {
            _gameObject = gameObject;
        }

        private void OnDestroy()
        {
            OnSpawnPointIsFree = null;
            CancelInvoke();
        }

        public void SetPool(IGameObjectPool objectPool)
        {
            _objectPool = objectPool;
        }

        public int Collect()
        {
            _gameObject.SetActive(false);
            _objectPool.BackToPool(_gameObject);
            
            var respawnTime = GetRespawnTime();
            Invoke(nameof(CanBeRespawned), respawnTime);
            
            return _collectibleConfig.Collectible.Score;
        }
        
        protected float GetRespawnTime()
        {
            var respawnInfo = _collectibleConfig.Collectible.RespawnTime;
            return Random.Range(respawnInfo.MinTime, respawnInfo.MaxTime);
        }
        
        protected void CanBeRespawned()
        {
            OnSpawnPointIsFree?.Invoke(this);
        }
    }
}