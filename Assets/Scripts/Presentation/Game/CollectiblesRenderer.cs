using System.Collections.Generic;
using Collectibles.Config;
using Collectibles.Spawner;
using Services;
using Services.GameObjectPooling;
using UnityEngine;

namespace Presentation.Game
{
    public class CollectiblesRenderer : MonoBehaviour
    {
        private const string TagCollectibleSpawnPoint = "CollectibleSpawnPoint";
        
        [Header("Configs")]
        [SerializeField] private CollectibleConfig[] _collectibleConfigs;
        
        [Header("Dependencies")]
        [SerializeField] private GameObjectPoolCollection _poolsCollection;
        
        private ICollectiblesSpawner _collectiblesSpawner;
        private CollectibleConfig[] _collectiblesConfig;
        private IEnumerable<GameObject> _spawnPoints;

        private void Awake()
        {
            _poolsCollection.Initialize(true);

            _collectiblesSpawner = ServiceLocator.Instance.GetService<ICollectiblesSpawner>();
            _collectiblesSpawner.SetCollectiblesConfigs(_collectibleConfigs);
        }
        
        public void LoadSpawnPoints()
        {
            _spawnPoints = GameObject.FindGameObjectsWithTag(TagCollectibleSpawnPoint);
        }

        public void SpawnCollectibles()
        {
            foreach (var spawnPoint in _spawnPoints)
            {
                var collectibleType = _collectiblesSpawner.GetRandomCollectibleTypeToSpawn();
                _poolsCollection.GetInstance(collectibleType, spawnPoint.transform.position);
            }
        }
    }
}