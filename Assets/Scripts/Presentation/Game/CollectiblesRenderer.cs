using System.Collections;
using System.Collections.Generic;
using Collectibles.Config;
using Collectibles.Controllers;
using Collectibles.Spawner;
using Game;
using Game.Signals;
using Services;
using Services.EventDispatcher;
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
        private IEventDispatcher _eventDispatcher;
        private IGame _game;

        private void Awake()
        {
            _poolsCollection.Initialize(true);

            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            _collectiblesSpawner = ServiceLocator.Instance.GetService<ICollectiblesSpawner>();
            _game = ServiceLocator.Instance.GetService<IGame>();
            
            _collectiblesSpawner.SetCollectiblesConfigs(_collectibleConfigs);
            
            _eventDispatcher.Subscribe<RespawnCollectibleSignal>(SpawnNewCollectible);
        }

        private void OnDestroy()
        {
            _eventDispatcher.Unsubscribe<RespawnCollectibleSignal>(SpawnNewCollectible);
        }

        public void LoadSpawnPoints()
        {
            _spawnPoints = GameObject.FindGameObjectsWithTag(TagCollectibleSpawnPoint);
        }

        public void SpawnCollectibles()
        {
            foreach (var spawnPoint in _spawnPoints)
            {
                SpawnCollectible(spawnPoint.transform.position);
            }
        }
        
        private void SpawnCollectible(Vector3 spawnPosition)
        {
            if (_game.IsGameOver) { return; }
            
            var collectibleType = _collectiblesSpawner.GetRandomCollectibleTypeToSpawn();
            var instance = _poolsCollection.GetInstance(collectibleType, spawnPosition);
            
            var iCollectible = instance.GetComponent<ICollectible>();
            iCollectible.HandleSpawn();
        }

        private void SpawnNewCollectible(RespawnCollectibleSignal signal)
        {
            StartCoroutine(DelaySpawnCollectible(signal.RespawnTime, signal.Position));
        }

        private IEnumerator DelaySpawnCollectible(float respawnTime, Vector3 position)
        {
            yield return new WaitForSeconds(respawnTime);
            
            SpawnCollectible(position);
        }
    }
}