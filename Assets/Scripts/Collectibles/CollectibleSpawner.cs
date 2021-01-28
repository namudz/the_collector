using UnityEngine;

namespace Collectibles
{
    public class CollectibleSpawner : Spawner
    {
        [Header("Components")]
        [SerializeField] private GameObject _collectiblePrefab;
        
        public override void Spawn()
        {
            var instance = Instantiate(_collectiblePrefab, _spawnPoint.position, Quaternion.identity, transform);
            instance.SetActive(true);
        }
    }
}