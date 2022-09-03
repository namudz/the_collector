using DomainLayer.Collectibles;
using UnityEngine;

namespace Collectibles.Config
{
    [CreateAssetMenu(fileName = "Collectible", menuName = "ScriptableObjects/Collectible Config/Coin", order = 1)]
    public class CoinConfig : CollectibleConfig
    {
        [SerializeField] private Coin _config;
        public override Collectible Collectible => _config;
    }
}