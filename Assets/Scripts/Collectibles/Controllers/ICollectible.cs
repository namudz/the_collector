using InterfaceAdapters.Game;

namespace Collectibles.Controllers
{
    public interface ICollectible
    {
        ItemType Type { get; }
        void HandleSpawn();
        int Collect();
    }
}