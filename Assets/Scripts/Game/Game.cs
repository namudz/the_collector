using EventDispatcher;
using SceneLoader;
using UnityEngine;

public class Game : IGame
{
    public SceneConstants.Mazes CurrentLevel { get; private set; }
    
    private readonly IMazeLoader _mazeLoader;
    private readonly IGameCountdownTimer _countdownTimer;
    private readonly IEventDispatcher _eventDispatcher;

    private bool _hasGameStarted;
    private bool _isGameOver;

    public Game(IMazeLoader mazeLoader, IGameCountdownTimer countdownTimer, IEventDispatcher eventDispatcher)
    {
        _mazeLoader = mazeLoader;
        _countdownTimer = countdownTimer;
        _eventDispatcher = eventDispatcher;
        _hasGameStarted = false;
        _isGameOver = false;

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
        _hasGameStarted = true;
    }

    public void Tick()
    {
        if (_hasGameStarted && !_isGameOver)
        {
            _countdownTimer.UpdateCountdown();
        }
    }
    
    public void Reset()
    {
        Debug.LogError("TODO - Implement IGame.Reset");
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
        _isGameOver = true;
    }
}