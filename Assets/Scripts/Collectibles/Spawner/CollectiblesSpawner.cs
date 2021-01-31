using System.Collections.Generic;
using Collectibles.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Collectibles.Spawner
{
    public class CollectiblesSpawner : ICollectiblesSpawner
    {
        private IEnumerable<GameObject> _spawnPoints;
        private IEnumerable<CollectibleConfig> _collectibleConfigs;

        private readonly CoinPool _coinPool;
        private readonly ChestPool _chestPool;
        private float _totalSpawnWeight;

        public CollectiblesSpawner(CoinPool coinPool, ChestPool chestPool)
        {
            _coinPool = coinPool;
            _chestPool = chestPool;
        }

        public void SetAvailableCollectibles(IEnumerable<CollectibleConfig> collectibles)
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
                var collectible = GetCollectibleToSpawn();
                switch (collectible.Type)
                {
                    case Collectible.CollectibleType.Coin:
                        _coinPool.GetInstance(spawnPoint.transform.position);
                        break;
                    case Collectible.CollectibleType.Chest:
                        _chestPool.GetInstance(spawnPoint.transform.position);
                        break;
                }
            }
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