using InterfaceAdapters.Services.EventDispatcher;
using InterfaceAdapters.Signals;

namespace InterfaceAdapters.Game
{
    public class Game : IGame
    {
        public Level.Level CurrentLevel { get; set; }
        public bool HasGameStarted { get; private set; }
        public bool IsGameOver { get; private set; }
    
        private readonly IGameCountdownTimer _countdownTimer;
        private readonly IGameScoreboard _gameScoreboard;
        private readonly IEventDispatcher _eventDispatcher;

        public Game(
            IGameCountdownTimer countdownTimer, 
            IGameScoreboard gameScoreboard,
            IEventDispatcher eventDispatcher)
        {
            _countdownTimer = countdownTimer;
            _gameScoreboard = gameScoreboard;
            _eventDispatcher = eventDispatcher;
            HasGameStarted = false;
            IsGameOver = false;

            _countdownTimer.OnCountdownFinished += HandleGameOver;
        }

        public void GetReady()
        {
            _countdownTimer.SetInitialCountdown(CurrentLevel.Countdown);
            
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