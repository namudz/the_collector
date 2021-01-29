using UnityEngine;

namespace Collectibles
{
    public class CollectibleController : MonoBehaviour, ICollectible
    {
        [SerializeField] private CollectibleConfig _collectibleConfig;

        public Collectible Collect()
        {
            ActivateGameObject(false);
            var respawnTime = GetRespawnTime();
            Invoke(nameof(Spawn), respawnTime);
            
            return _collectibleConfig.Collectible;
        }

        public void Spawn()
        {
            ActivateGameObject(true);
        }
        
        public float GetRespawnTime()
        {
            var respawnInfo = _collectibleConfig.Collectible.RespawnTime;
            return Random.Range(respawnInfo.MinTime, respawnInfo.MaxTime);
        }

        private void ActivateGameObject(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}