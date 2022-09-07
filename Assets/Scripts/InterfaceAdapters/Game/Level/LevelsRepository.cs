using System.Collections.Generic;
using UnityEngine.Assertions;

namespace InterfaceAdapters.Game.Level
{
    public class LevelsRepository : ILevelsRepository
    {
        private readonly List<Level> _levels;

        public LevelsRepository()
        {
            _levels = new List<Level>();
        }
        
        public void AddLevel(Level newLevel)
        {
            if (_levels.Find(level => level.Id == newLevel.Id) == null)
            {
                _levels.Add(newLevel);
            }
        }

        public int LevelsCount => _levels.Count;

        public Level GetLevel(int index)
        {
            Assert.IsTrue(index >= 0, "Level index can't be < 0");
            return _levels[index];
        }

        public Level GetLevel(string levelId)
        {
            return _levels.Find(level => level.Id == levelId);
        }
    }
}