using Game;
using InterfaceAdapters.Signals;
using Services;
using Services.EventDispatcher;
using UnityEngine;

namespace Presentation.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private MazeController _mazeController;
        
        private IEventDispatcher _eventDispatcher;
        private IGame _game;

        private void Awake()
        {
            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
        }

        public void InjectDependencies(IGame game)
        {
            _game = game;
        }

        public void Load(string mazeSceneName)
        {
            _mazeController.LoadMaze(mazeSceneName, GetReady);
        }
        
        private void GetReady()
        {
            _game.GetReady();
            _eventDispatcher.Dispatch(new ShowLoadingScreenSignal(false));
        }

        private void Update()
        {
            _game.Tick();
        }

        private void Reset()
        {
            _game.Reset();
        }
    }
}