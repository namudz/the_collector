using System;
using Collectibles.Config;

namespace Collectibles.Controllers
{
    public interface ICollectible
    {
        event Action<CollectibleController> OnSpawnPointIsFree;
        Collectible.CollectibleType Type { get; }
        void HandleSpawn();
        int Collect();
    }
}