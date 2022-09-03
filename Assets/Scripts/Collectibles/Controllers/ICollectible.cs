using System;
using DomainLayer.Collectibles;
using InterfaceAdapters.Game;

namespace Collectibles.Controllers
{
    public interface ICollectible
    {
        event Action<CollectibleController> OnSpawnPointIsFree;
        ItemType Type { get; }
        void HandleSpawn();
        int Collect();
    }
}