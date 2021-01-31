using System;
using Collectibles.Controllers;
using Collectibles.Pool;

namespace Collectibles
{
    public interface ICollectible
    {
        event Action<CollectibleController> OnSpawnPointIsFree;
        void SetPool(IGameObjectPool gameObjectPool);
        Collectible.CollectibleType Type { get; }
        int Collect();
    }
}