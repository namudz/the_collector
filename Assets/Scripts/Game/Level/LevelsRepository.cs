using System.Collections.Generic;

namespace Game.Level
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

        public Level GetLevel(string levelId)
        {
            return _levels.Find(level => level.Id == levelId);
        }
    }
}