using EventDispatcher;
using Game;
using SceneLoader;
using UnityEngine;

namespace Presentation.Installers
{
    public class GameInstaller : MonoBehaviour
    {
        private IGame _game;

        private void Awake()
        {
            InitializeDependencies();
        }

        private void Start()
        {
            _game.Load();
        }

        private void Update()
        {
            _game.Tick();
        }

        private void InitializeDependencies()
        {
            var eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            
            var gameScoreboard = new GameScoreboard();
            ServiceLocator.Instance.RegisterService<IGameScoreboard>(gameScoreboard);
            
            var gameCountdownTimer = new GameCountdownTimer();
            ServiceLocator.Instance.RegisterService<IGameCountdownTimer>(gameCountdownTimer);

            var mazeLoader = new MazeLoader(ServiceLocator.Instance.GetService<ISceneLoader>());
            _game = new global::Game.Game(mazeLoader, gameCountdownTimer, eventDispatcher);
            ServiceLocator.Instance.RegisterService(_game);
        }
    }
}