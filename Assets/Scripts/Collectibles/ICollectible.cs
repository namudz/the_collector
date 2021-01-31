using System;
using Collectibles.Controllers;

namespace Collectibles
{
    public interface ICollectible
    {
        event Action<CollectibleController> OnSpawnPointIsFree;
        Collectible.CollectibleType Type { get; }
        int Collect();
    }
}