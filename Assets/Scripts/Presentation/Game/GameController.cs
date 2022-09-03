using Game;
using InterfaceAdapters.Signals;
using Services;
using Services.EventDispatcher;
using UnityEngine;

namespace Presentation.Game
{
    public class GameController : MonoBehaviour
    {
        private IEventDispatcher _eventDispatcher;
        private IMazeLoader _mazeLoader;
        private IGame _game;

        private void Awake()
        {
            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
        }

        public void InjectDependencies(IMazeLoader mazeLoader, IGame game)
        {
            _mazeLoader = mazeLoader;
            _game = game;
        }

        public void Load(string mazeSceneName)
        {
            _mazeLoader.Load(mazeSceneName, GetReady);
        }
        
        private void Update()
        {
            _game.Tick();
        }

        private void GetReady()
        {
            _mazeLoader.SpawnElements();
            _game.GetReady();
            Reset();
            
            _eventDispatcher.Dispatch(new ShowLoadingScreenSignal(false));
        }

        private void Reset()
        {
            _game.Reset();
            _mazeLoader.Reset();
        }
    }
}