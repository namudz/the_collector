using InterfaceAdapters.Game.Level;

namespace PresentationLayer.ScriptableObjects
{
    public interface ICollectibleConfig
    {
        CollectibleDto Collectible { get; }
    }
}