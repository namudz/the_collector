using System.Collections.Generic;
using Collectibles.Config;
using Collectibles.Controllers;
using DomainLayer.Collectibles;
using Services.Pooling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Collectibles.Spawner
{
    public class CollectiblesSpawner : ICollectiblesSpawner
    {
        private IEnumerable<GameObject> _spawnPoints;
        private IEnumerable<ICollectibleConfig> _collectibleConfigs;

        /*private readonly IGameObjectPool<CoinCollectible> _coinPool;
        private readonly IGameObjectPool<ChestCollectible> _chestPool;*/
        private float _totalSpawnWeight;

        /*public CollectiblesSpawner(IGameObjectPool<CoinCollectible> coinPool, IGameObjectPool<ChestCollectible> chestPool)
        {
            _coinPool = coinPool;
            _chestPool = chestPool;
        }*/

        public void SetCollectiblesConfigs(IEnumerable<ICollectibleConfig> collectibles)
        {
            _collectibleConfigs = collectibles;
            CalcTotalSpawnWeight();
        }

        public void SetSpawnPoints(IEnumerable<GameObject> points)
        {
            _spawnPoints = points;
        }
        
        public void Spawn()
        {
            foreach (var spawnPoint in _spawnPoints)
            {
                SpawnCollectible(spawnPoint.transform.position);
            }
        }

        public void Reset()
        {
            //throw new System.NotImplementedException();
        }

        private void SpawnCollectible(Vector3 position)
        {
            var collectible = GetCollectibleToSpawn();
            GameObject collectibleInstance = null;
            /*switch (collectible.Type)
            {
                case Collectible.CollectibleType.Coin:
                    collectibleInstance = _coinPool.GetInstance(position);
                    break;
                case Collectible.CollectibleType.Chest:
                    collectibleInstance = _chestPool.GetInstance(position);
                    break;
            }*/

            if (collectibleInstance != null)
            {
                var iCollectible = collectibleInstance.GetComponent<ICollectible>();
                iCollectible.HandleSpawn();
                iCollectible.OnSpawnPointIsFree += RespawnNewCollectible;
            }
        }

        private void RespawnNewCollectible(CollectibleController instanceCollected)
        {
            instanceCollected.OnSpawnPointIsFree -= RespawnNewCollectible;
            SpawnCollectible(instanceCollected.transform.position);
        }

        private void CalcTotalSpawnWeight()
        {
            _totalSpawnWeight = 0;
            foreach (var collectible in _collectibleConfigs)
            {
                _totalSpawnWeight += collectible.Collectible.SpawnWeight;
            }
        }

        private Collectible GetCollectibleToSpawn()
        {
            var randomValue = Random.Range(0f, 1f);
            var deltaChances = 0f;
            foreach (var config in _collectibleConfigs)
            {
                var collectibleChance = config.Collectible.SpawnWeight / _totalSpawnWeight;
                if (randomValue <= deltaChances + collectibleChance)
                {
                    return config.Collectible;
                }

                deltaChances += collectibleChance;
            }

            return null;
        }
    }
}