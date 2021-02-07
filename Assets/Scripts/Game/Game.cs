using Game.Signals;
using InterfaceAdapters.Signals;
using Services.EventDispatcher;

namespace Game
{
    public class Game : IGame
    {
        public string CurrentLevelId => _currentLevel.Id;
        public bool HasGameStarted { get; private set; }
        public bool IsGameOver { get; private set; }
    
        private readonly IMazeLoader _mazeLoader;
        private readonly IGameCountdownTimer _countdownTimer;
        private readonly IGameScoreboard _gameScoreboard;
        private readonly IEventDispatcher _eventDispatcher;
        private Level.Level _currentLevel;

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

        public void SetCurrentLevelData(Level.Level level)
        {
            _currentLevel = level;
        }

        public void Load()
        {
            _mazeLoader.Load(_currentLevel.SceneName, GetReady);
            _countdownTimer.SetInitialCountdown(_currentLevel.Countdown);
        }

        public void GetReady()
        {
            ResetComponents();
            _mazeLoader.SpawnElements();
            _eventDispatcher.Dispatch(new GameReadySignal());
            _eventDispatcher.Dispatch(new ShowLoadingScreenSignal(false));
        }
    
        public void Start()
        {
            HasGameStarted = true;
            _countdownTimer.StartCountdown();
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
            _eventDispatcher.Dispatch(new GameResetSignal());
            _mazeLoader.Reset();
        }
        
        private void ResetComponents()
        {
            HasGameStarted = false;
            IsGameOver = false;
            _gameScoreboard.Reset();
        }

        private void HandleGameOver()
        {
            IsGameOver = true;
            _eventDispatcher.Dispatch(new GameOverSignal());
        }
    }
}