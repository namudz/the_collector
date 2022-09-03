using System.Linq;
using DomainLayer;
using Game.Level;
using UnityEngine;

namespace InterfaceAdapters.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "ScriptableObjects/Levels Config", order = 1)]
    public class LevelsConfig : ScriptableObject
    {
        public LevelConfig[] Configurations;

        public Level[] GetLevels()
        {
            return Configurations.Select(config => config.Level).ToArray();
        }
    }
}