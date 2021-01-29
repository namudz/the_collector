using System;
using Collectibles;
using UnityEngine;
using UnityEngine.Assertions;

namespace Hero
{
    public class HeroCollector : MonoBehaviour
    {
        [Header("Collectibles")]
        [SerializeField] private LayerMask _collectibleLayerMask;

        private IGameScoreboard _gameScoreboard;

        public void InjectDependencies(IGameScoreboard gameScoreboard)
        {
            _gameScoreboard = gameScoreboard;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((_collectibleLayerMask & 1 << other.gameObject.layer) != 0)
            {
                CollectItem(other.gameObject);
            }
        }

        private void CollectItem(GameObject otherGameObject)
        {
            var iCollectible = otherGameObject.GetComponent<ICollectible>();
            Assert.IsNotNull(iCollectible, "The Collectible item don't have an ICollectible attached!");
            
            var item = iCollectible.Collect();
            _gameScoreboard.AddScore(item.Score);
        }
    }
}