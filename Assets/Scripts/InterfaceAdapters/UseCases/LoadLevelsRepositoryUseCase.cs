using System.Collections.Generic;
using DomainLayer;
using Game.Level;
using Services.DataPersistence;

namespace InterfaceAdapters.UseCases
{
    public class LoadLevelsRepositoryUseCase : ILoadLevelsRepositoryUseCase
    {
        private readonly ILevelsRepository _levelsRepository;
        private readonly IDataPersistence _dataPersistence;

        public LoadLevelsRepositoryUseCase(
            ILevelsRepository levelsRepository, 
            IDataPersistence dataPersistence)
        {
            _levelsRepository = levelsRepository;
            _dataPersistence = dataPersistence;
        }
        
        public void LoadLevels(IEnumerable<Level> levels)
        {
            foreach (var level in levels)
            {
                var leaderboard = _dataPersistence.GetLevelLeaderboard(level.Id);
                if (leaderboard == null)
                {
                    level.InitializeLevelLeaderboard(level.Id);
                }
                else
                {
                    level.Leaderboard = leaderboard;
                }
                
                _levelsRepository.AddLevel(level);
            }
        }
    }
}