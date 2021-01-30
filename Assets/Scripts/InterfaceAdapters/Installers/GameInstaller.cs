using EventDispatcher;
using Game;
using SceneLoader;

namespace InterfaceAdapters.Installers
{
    public class GameInstaller : IGameInstaller
    {
        public void Register()
        {
            ServiceLocator.Instance.RegisterService(this);
        }

        public void InstallDependencies()
        {
            var eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            
            var gameScoreboard = new GameScoreboard();
            ServiceLocator.Instance.RegisterService<IGameScoreboard>(gameScoreboard);
            
            var gameCountdownTimer = new GameCountdownTimer();
            ServiceLocator.Instance.RegisterService<IGameCountdownTimer>(gameCountdownTimer);

            var mazeLoader = new MazeLoader(ServiceLocator.Instance.GetService<ISceneLoader>());
            var game = new global::Game.Game(
                mazeLoader, 
                gameCountdownTimer,
                gameScoreboard,
                eventDispatcher
            );
            ServiceLocator.Instance.RegisterService<IGame>(game);
        }
    }
}