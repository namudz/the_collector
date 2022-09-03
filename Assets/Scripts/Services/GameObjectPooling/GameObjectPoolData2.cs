using System;

namespace Services.GameObjectPooling
{
    [Serializable]
    public class GameObjectPoolData2
    {
        public string Id;
        public PoolableGameObject GameObject;
        public int InitialAmount;
    }
}