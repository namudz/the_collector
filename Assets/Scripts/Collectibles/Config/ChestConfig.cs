using UnityEngine;

namespace Collectibles.Config
{
    [CreateAssetMenu(fileName = "Collectible", menuName = "ScriptableObjects/Collectible Config/Chest", order = 1)]
    public class ChestConfig : CollectibleConfig
    {
        [SerializeField] private Chest _config;
        public override Collectible Collectible => _config;
    }
}