using System;

namespace InterfaceAdapters.Services.GameObjectPooling
{
    [Serializable]
    public class GameObjectPoolData2
    {
        public string Id;
        public PoolableGameObject GameObject;
        public int InitialAmount;
    }
}