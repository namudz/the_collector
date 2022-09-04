using System.Collections.Generic;
using Collectibles.Config;

namespace Collectibles.Spawner
{
    public interface ICollectiblesSpawner : ISpawner
    {
        void SetCollectiblesConfigs(IEnumerable<ICollectibleConfig> collectibles);
        string GetRandomCollectibleTypeToSpawn();
    }
}