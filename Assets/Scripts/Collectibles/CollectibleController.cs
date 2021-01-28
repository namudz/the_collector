using UnityEngine;

namespace Collectibles
{
    public class CollectibleController : MonoBehaviour, ICollectible
    {
        [SerializeField] private CollectibleConfig _collectibleConfig;
        
        public Collectible Collect()
        {
            return _collectibleConfig.Collectible;
        }
    }
}