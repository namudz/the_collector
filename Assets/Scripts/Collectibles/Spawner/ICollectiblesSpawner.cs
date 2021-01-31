using System.Collections.Generic;
using UnityEngine;

namespace Collectibles.Spawner
{
    public interface ICollectiblesSpawner : ISpawner
    {
        void SetAvailableCollectibles(IEnumerable<CollectibleConfig> collectibles);
        void SetSpawnPoints(IEnumerable<GameObject> points);
    }
}