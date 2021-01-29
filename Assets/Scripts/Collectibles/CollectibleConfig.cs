using UnityEngine;

namespace Collectibles
{
    [CreateAssetMenu(fileName = "Collectible", menuName = "ScriptableObjects/Collectible Config", order = 1)]
    public class CollectibleConfig : ScriptableObject
    {
        public Collectible Collectible;
    }
}