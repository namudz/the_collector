﻿using InterfaceAdapters.Game;
using InterfaceAdapters.Game.Level;
using InterfaceAdapters.Services.DataPersistence;

namespace InterfaceAdapters
{
    public class SaveScoreUseCase : ISaveScoreUseCase
    {
        private readonly IGame _game;
        private readonly IGameScoreboard _gameScoreboard;
        private readonly ILevelsRepository _levelsRepository;
        private readonly IDataPersistence _dataPersistence;

        public SaveScoreUseCase(
            IGame game, 
            IGameScoreboard gameScoreboard, 
            ILevelsRepository levelsRepository,
            IDataPersistence dataPersistence)
        {
            _game = game;
            _gameScoreboard = gameScoreboard;
            _levelsRepository = levelsRepository;
            _dataPersistence = dataPersistence;
        }
        
        public void SaveScore(string userName)
        {
            var level = _levelsRepository.GetLevel(_game.CurrentLevel.Id);
            for (var i = 0; i < level.Leaderboard.Entries.Count; i++)
            {
                var entry = level.Leaderboard.Entries[i];
                if (_gameScoreboard.CurrentScore <= entry.Score) { continue;}
                
                var newEntry = new LeaderboardEntry
                {
                    Score = _gameScoreboard.CurrentScore,
                    UserName = userName
                };
                level.Leaderboard.AddEntry(newEntry, i);
                break;
            }
            
            _dataPersistence.SaveLevelLeaderboard(level.Leaderboard);
        }
    }

    public interface ISaveScoreUseCase
    {
        void SaveScore(string userName);
    }
}