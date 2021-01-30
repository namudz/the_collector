﻿using EventDispatcher;
using Game.Signals;
using SceneLoader;
using UnityEngine;

namespace Game
{
    public class Game : IGame
    {
        public SceneConstants.Mazes CurrentLevel { get; private set; }
        public bool HasGameStarted { get; private set; }
        public bool IsGameOver { get; private set; }
    
        private readonly IMazeLoader _mazeLoader;
        private readonly IGameCountdownTimer _countdownTimer;
        private readonly IGameScoreboard _gameScoreboard;
        private readonly IEventDispatcher _eventDispatcher;

        public Game(
            IMazeLoader mazeLoader, 
            IGameCountdownTimer countdownTimer, 
            IGameScoreboard gameScoreboard,
            IEventDispatcher eventDispatcher)
        {
            _mazeLoader = mazeLoader;
            _countdownTimer = countdownTimer;
            _gameScoreboard = gameScoreboard;
            _eventDispatcher = eventDispatcher;
            HasGameStarted = false;
            IsGameOver = false;

            _countdownTimer.OnCountdownFinished += HandleGameOver;
        }

        public void Load()
        {
            CurrentLevel = SceneConstants.Mazes.Level_1;
            _mazeLoader.Load(CurrentLevel, Start);
            // TODO - If enough time, add Loading Canvas to show when loading the level & its fully loaded
        }
    
        public void Start()
        {
            SpawnCollectibles();
            SpawnHero();
            _countdownTimer.StartCountdown();
            ResetComponents();

            _eventDispatcher.Dispatch(new GameStartedSignal());
        }

        public void Tick()
        {
            if (HasGameStarted && !IsGameOver)
            {
                _countdownTimer.UpdateCountdown();
            }
        }
    
        public void Reset()
        {
            ResetComponents();
            Debug.LogError("TODO - Implement IGame.Reset");
        }
        
        private void ResetComponents()
        {
            HasGameStarted = true;
            IsGameOver = false;
            _gameScoreboard.Reset();
        }

        private void SpawnCollectibles()
        {
            foreach (var spawner in _mazeLoader.CollectibleSpawners)
            {
                spawner.Spawn();
            }
        }
    
        private void SpawnHero()
        {
            _mazeLoader.HeroSpawner.Spawn();
        }

        private void HandleGameOver()
        {
            IsGameOver = true;
            _eventDispatcher.Dispatch(new GameOverSignal());
        }
    }
}