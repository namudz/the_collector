using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Collectibles
{
    public class CollectibleController : MonoBehaviour
    {
        public event Action<CollectibleController, float> OnCollected;
        public Collectible.CollectibleType Type => _collectibleConfig.Collectible.Type;
        
        [SerializeField] private CollectibleConfig _collectibleConfig;

        public Collectible Collect()
        {
            ActivateGameObject(false);
            OnCollected?.Invoke(this, GetRespawnTime());
            return _collectibleConfig.Collectible;
        }

        public void Spawn()
        {
            ActivateGameObject(true);
        }
        
        private void ActivateGameObject(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
        
        private float GetRespawnTime()
        {
            var respawnInfo = _collectibleConfig.Collectible.RespawnTime;
            return Random.Range(respawnInfo.MinTime, respawnInfo.MaxTime);
        }
    }
}