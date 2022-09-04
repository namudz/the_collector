using UnityEngine;

namespace InterfaceAdapters.Game.Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/Level Config", order = 1)]
    public class LevelConfig : ScriptableObject
    {
        public Level Level;
    }
}