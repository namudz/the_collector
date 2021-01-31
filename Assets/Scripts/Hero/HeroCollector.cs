using Collectibles;
using Game;
using UnityEngine;
using UnityEngine.Assertions;

namespace Hero
{
    public class HeroCollector : MonoBehaviour
    {
        [Header("Collectibles")]
        [SerializeField] private LayerMask _collectibleLayerMask;

        private IGameScoreboard _gameScoreboard;
        private IGame _iGame;

        private void Awake()
        {
            _gameScoreboard = ServiceLocator.Instance.GetService<IGameScoreboard>();
            _iGame = ServiceLocator.Instance.GetService<IGame>();
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
            if (!_iGame.HasGameStarted || _iGame.IsGameOver) { return; }
            
            var iCollectible = otherGameObject.GetComponent<ICollectible>();
            Assert.IsNotNull(iCollectible, "The Collectible item don't have an ICollectible attached!");
            
            var points = iCollectible.Collect();
            _gameScoreboard.AddScore(points);
        }
    }
}