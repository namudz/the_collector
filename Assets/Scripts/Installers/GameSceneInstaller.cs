using InterfaceAdapters.Game;
using InterfaceAdapters.Services;
using InterfaceAdapters.Services.EventDispatcher;
using InterfaceAdapters.Services.InputHandler;
using InterfaceAdapters.Services.SceneLoader;
using InterfaceAdapters.Signals;
using PresentationLayer.Game.Collectibles;
using UnityEngine;

namespace Installers
{
    public class GameSceneInstaller : MonoBehaviour
    {
        private IMazeLoader _mazeLoader;
        private ICollectiblesSpawner _collectiblesSpawner;
        private IEventDispatcher _eventDispatcher;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            InitializeDependencies();
            
            _eventDispatcher.Subscribe<SceneLoadedSignal>(TryLoadGame);
        }

        private void OnDestroy()
        {
            _eventDispatcher.Unsubscribe<SceneLoadedSignal>(TryLoadGame);
        }

        private void InitializeDependencies()
        {
            _collectiblesSpawner = new CollectiblesSpawner();
            ServiceLocator.Instance.RegisterService(_collectiblesSpawner);
            
            _mazeLoader = new MazeLoader(ServiceLocator.Instance.GetService<ISceneLoader>());
            ServiceLocator.Instance.RegisterService(_mazeLoader);
            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();

            InitializeInputHandler();
        }
        
        private static void InitializeInputHandler()
        {
            IInputHandler handler;
#if UNITY_EDITOR || UNITY_STANDALONE
            handler = new InputStandaloneHandler();
#elif UNITY_ANDROID || UNITY_IOS
            handler = new InputMobileHandler();
#endif
            ServiceLocator.Instance.RegisterService(handler);
        }

        private void TryLoadGame(SceneLoadedSignal signal)
        {
            if (signal.Scene != SceneConstants.Scene.Game)
            {
                return;
            }
            
            _eventDispatcher.Dispatch(new GameSceneLoadedSignal());
        }
    }
}