using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Game.Level
{
    public class LevelsRepository : ILevelsRepository
    {
        private readonly List<DomainLayer.Level> _levels;

        public LevelsRepository()
        {
            _levels = new List<DomainLayer.Level>();
        }
        
        public void AddLevel(DomainLayer.Level newLevel)
        {
            if (_levels.Find(level => level.Id == newLevel.Id) == null)
            {
                _levels.Add(newLevel);
            }
        }
        
        public DomainLayer.Level GetLevel(int index)
        {
            Assert.IsTrue(index >= 0, "Level index can't be < 0");
            return _levels[index];
        }

        public DomainLayer.Level GetLevel(string levelId)
        {
            return _levels.Find(level => level.Id == levelId);
        }
    }
}