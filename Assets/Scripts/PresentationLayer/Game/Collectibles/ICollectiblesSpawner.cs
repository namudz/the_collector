using System.Collections.Generic;
using PresentationLayer.Game.Spawners;
using PresentationLayer.ScriptableObjects;

namespace PresentationLayer.Game.Collectibles
{
    public interface ICollectiblesSpawner : ISpawner
    {
        void SetCollectiblesConfigs(IEnumerable<ICollectibleConfig> collectibles);
        string GetRandomCollectibleTypeToSpawn();
    }
}