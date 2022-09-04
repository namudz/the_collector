using DomainLayer;

namespace InterfaceAdapters.Game
{
    public interface ICollectible
    {
        string Type { get; }
        void HandleSpawn();
        int Collect();
    }
}