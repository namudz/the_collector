using UnityEngine;

namespace Collectibles.Pool
{
    [CreateAssetMenu(fileName = "GameObjectPoolData", menuName = "ScriptableObjects/GameObject Pool Config", order = 0)]
    public class GameObjectPoolDataConfig : ScriptableObject
    {
        public GameObjectPoolData PoolData;
    }
}