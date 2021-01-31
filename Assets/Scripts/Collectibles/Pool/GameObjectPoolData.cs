using System;
using UnityEngine;

namespace Collectibles.Pool
{
    [Serializable]
    public class GameObjectPoolData
    {
        public Transform RootTransform { get; set; }
        public GameObject Prefab;
        public int InitialAmount;
    }
}