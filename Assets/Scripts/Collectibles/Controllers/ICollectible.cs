using System;
using Collectibles.Config;
using Collectibles.Pool;

namespace Collectibles.Controllers
{
    public interface ICollectible
    {
        event Action<CollectibleController> OnSpawnPointIsFree;
        void SetPool(IGameObjectPool gameObjectPool);
        Collectible.CollectibleType Type { get; }
        void HandleSpawn();
        int Collect();
    }
}