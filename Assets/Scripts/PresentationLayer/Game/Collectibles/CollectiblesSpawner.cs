using System.Collections.Generic;
using PresentationLayer.ScriptableObjects;
using Random = UnityEngine.Random;

namespace PresentationLayer.Game.Collectibles
{
    public class CollectiblesSpawner : ICollectiblesSpawner
    {
        private IEnumerable<ICollectibleConfig> _collectibleConfigs;

        private float _totalSpawnWeight;

        public void SetCollectiblesConfigs(IEnumerable<ICollectibleConfig> collectibles)
        {
            _collectibleConfigs = collectibles;
            CalcTotalSpawnWeight();
        }
        
        public void Spawn()
        {
        }

        public void Reset()
        {
        }

        private void CalcTotalSpawnWeight()
        {
            _totalSpawnWeight = 0;
            foreach (var collectible in _collectibleConfigs)
            {
                _totalSpawnWeight += collectible.Collectible.SpawnWeight;
            }
        }

        public string GetRandomCollectibleTypeToSpawn()
        {
            var randomValue = Random.Range(0f, 1f);
            var deltaChances = 0f;
            foreach (var config in _collectibleConfigs)
            {
                var collectibleChance = config.Collectible.SpawnWeight / _totalSpawnWeight;
                if (randomValue <= deltaChances + collectibleChance)
                {
                    return config.Collectible.Type;
                }

                deltaChances += collectibleChance;
            }

            return string.Empty;
        }
    }
}