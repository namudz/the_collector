using InterfaceAdapters.Game.Level;
using UnityEngine;

namespace PresentationLayer.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Collectible", menuName = "ScriptableObjects/Collectible Config/Coin", order = 1)]
    public class CoinConfig : CollectibleConfig
    {
        [SerializeField] private CoinDto _config;
        public override CollectibleDto Collectible => _config;
    }
}