using Collectibles.Controllers;
using EventDispatcher;

namespace Collectibles.Pool
{
    public class ChestPool : GameObjectPool<ChestCollectible>
    {
        public ChestPool(GameObjectPoolData data) : base(data, ServiceLocator.Instance.GetService<IEventDispatcher>())
        {
             
        }
    }
}