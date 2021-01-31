using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Collectibles.Controllers
{
    public abstract class ACollectibleController : MonoBehaviour, ICollectible
    {
        public event Action<ACollectibleController> OnSpawnPointIsFree;
        public Collectible.CollectibleType Type => _collectibleConfig.Collectible.Type;
        
        [SerializeField] protected CollectibleConfig _collectibleConfig;

        public float GetRespawnTime()
        {
            var respawnInfo = _collectibleConfig.Collectible.RespawnTime;
            return Random.Range(respawnInfo.MinTime, respawnInfo.MaxTime);
        }

        public int Collect()
        {
            ActivateGameObject(false);
            var respawnTime = GetRespawnTime();
            Invoke(nameof(CanBeRespawned), respawnTime);
            return _collectibleConfig.Collectible.Score;
        }

        public void Spawn()
        {
            ActivateGameObject(true);
        }
        
        protected void ActivateGameObject(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        protected void CanBeRespawned()
        {
            OnSpawnPointIsFree?.Invoke(this);
        }
    }
}