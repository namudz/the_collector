using System.Collections.Generic;
using Collectibles.Config;
using UnityEngine;

namespace Collectibles.Spawner
{
    public interface ICollectiblesSpawner : ISpawner
    {
        void SetAvailableCollectibles(IEnumerable<ICollectibleConfig> collectibles);
        void SetSpawnPoints(IEnumerable<GameObject> points);
    }
}