﻿using Collectibles.Controllers;
using Services;
using Services.EventDispatcher;
using Services.Pooling;

namespace Collectibles.Pool
{
    public class ChestPool : GameObjectPool<ChestCollectible>
    {
        public ChestPool(GameObjectPoolData data) : base(data, ServiceLocator.Instance.GetService<IEventDispatcher>())
        {
             
        }
    }
}