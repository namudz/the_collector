using System;
using InterfaceAdapters.Game.Level;
using UnityEngine;

namespace PresentationLayer.ScriptableObjects
{
    [Serializable]
    public abstract class CollectibleConfig : ScriptableObject, ICollectibleConfig
    {
        public abstract CollectibleDto Collectible { get; }
    }
}