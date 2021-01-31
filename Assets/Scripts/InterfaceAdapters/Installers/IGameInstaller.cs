using Collectibles.Pool;
using UnityEngine;

namespace InterfaceAdapters.Installers
{
    public interface IGameInstaller : IInstaller
    {
        void LoadCollectibles(GameObjectPoolDataConfig[] collectiblePrefabs);
        void InitializePools(Transform poolParent);
    }
}