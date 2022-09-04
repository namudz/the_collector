using System.Collections.Generic;
using System.Linq;
using DomainLayer;
using InterfaceAdapters.Game.Level;
using UnityEngine;

namespace InterfaceAdapters.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "ScriptableObjects/Levels Config", order = 1)]
    public class LevelsConfig : ScriptableObject
    {
        public LevelConfig[] Configurations;

        public IEnumerable<Level> GetLevels()
        {
            return Configurations.Select(config => config.Level).ToArray();
        }
    }
}