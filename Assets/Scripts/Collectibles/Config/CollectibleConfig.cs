using System;
using DomainLayer.Collectibles;
using UnityEngine;

namespace Collectibles.Config
{
    [Serializable]
    public abstract class CollectibleConfig : ScriptableObject, ICollectibleConfig
    {
        public abstract Collectible Collectible { get; }
    }
}