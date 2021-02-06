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
            _mazeLoader.Load(_currentLevel.SceneName, () => Start());
            _countdownTimer.SetInitialCountdown(_currentLevel.Countdown);
            // TODO - If enough time, add Loading Canvas to show when loading the level & its fully loaded
        }
    
        public void Start(bool directStart = false)
        {
            if (directStart)
            {
                StartGame(null);
            }
            else
            {
                _eventDispatcher.Subscribe<LoadingHideDelayedFinished>(StartGame);
                _eventDispatcher.Dispatch(new ShowLoadingScreenSignal(false));    
            }
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

        private void StartGame(ISignal signal)
        {
            _mazeLoader.SpawnElements();
            ResetComponents();
            _countdownTimer.StartCountdown();
            _eventDispatcher.Dispatch(new GameStartedSignal());
        }
        
        private void ResetComponents()
        {
            HasGameStarted = true;
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