using InterfaceAdapters.Game.Level;
using UnityEngine;

namespace PresentationLayer.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Collectible", menuName = "ScriptableObjects/Collectible Config/Chest", order = 1)]
    public class ChestConfig : CollectibleConfig
    {
        [SerializeField] private ChestDto _config;
        public override CollectibleDto Collectible => _config;
    }
}