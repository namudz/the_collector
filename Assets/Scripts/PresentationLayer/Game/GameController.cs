using System;
using InterfaceAdapters.Game;
using InterfaceAdapters.Services;
using InterfaceAdapters.Services.EventDispatcher;
using InterfaceAdapters.Signals;
using UnityEngine;

namespace PresentationLayer.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private MazeController _mazeController;
        
        private IEventDispatcher _eventDispatcher;
        private IGame _game;

        private void Awake()
        {
            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            _game = ServiceLocator.Instance.GetService<IGame>();
            
            _eventDispatcher.Subscribe<GameSceneLoadedSignal>(Load);
        }

        private void OnDestroy()
        {
            _eventDispatcher.Unsubscribe<GameSceneLoadedSignal>(Load);
        }

        private void Load(ISignal _)
        {
            _game.Reset();
            _mazeController.LoadMaze(_game.CurrentLevel.SceneName, GetReady);
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
    }
}