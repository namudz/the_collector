using System.Linq;
using UnityEngine;

namespace InterfaceAdapters.Services.GameObjectPooling
{
    [CreateAssetMenu(fileName = "GameObjectPoolsDataConfig", menuName = "ScriptableObjects/Object Pool/GameObjects Pool Data Config", order = 0)]
    public class GameObjectPoolDataConfig2 : ScriptableObject
    {
        public GameObjectPoolData2[] Pools;

        public GameObjectPoolData2 GetPoolData(string objectId)
        {
            return Pools.FirstOrDefault(poolData => poolData.Id == objectId);
        }
    }
}