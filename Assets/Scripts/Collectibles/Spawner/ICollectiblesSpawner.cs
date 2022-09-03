using System.Collections.Generic;
using Collectibles.Config;
using UnityEngine;

namespace Collectibles.Spawner
{
    public interface ICollectiblesSpawner : ISpawner
    {
        void SetCollectiblesConfigs(IEnumerable<ICollectibleConfig> collectibles);
        void SetSpawnPoints(IEnumerable<GameObject> points);
    }
}