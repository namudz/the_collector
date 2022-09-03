using Collectibles.Config;
using Collectibles.Spawner;
using Game;
using InputHandler;
using InterfaceAdapters.Services.SceneLoader;
using Presentation.Game;
using Services;
using Services.EventDispatcher;
using UnityEngine;

namespace Installers
{
    public class GameInstaller_New : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private CollectibleConfig[] _collectibleConfigs;
        
        [Header("Dependencies")]
        [SerializeField] private GameController _gameController;
        
        private IMazeLoader _mazeLoader;
        private ICollectiblesSpawner _collectiblesSpawner;
        private IEventDispatcher _eventDispatcher;
        private IGame _game;

        private void Awake()
        {
            InitializeDependencies();
            InjectDependencies();
            
            _eventDispatcher.Subscribe<SceneLoadedSignal>(TryLoadGame);
        }

        private void OnDestroy()
        {
            _eventDispatcher.Unsubscribe<SceneLoadedSignal>(TryLoadGame);
        }

        private void TryLoadGame(ISignal iSignal)
        {
            var signal = (SceneLoadedSignal)iSignal;
            if (signal.Scene != SceneConstants.Scene.Game)
            {
                return;
            }
            
            LoadGame();
        }

        private void InitializeDependencies()
        {
            _game = ServiceLocator.Instance.GetService<IGame>();
            _collectiblesSpawner = new CollectiblesSpawner();
            _mazeLoader = new MazeLoader(ServiceLocator.Instance.GetService<ISceneLoader>(), _collectiblesSpawner);
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
        
        private void InjectDependencies()
        {
            _gameController.InjectDependencies(_mazeLoader, _game);
            _collectiblesSpawner.SetCollectiblesConfigs(_collectibleConfigs);
        }
        
        private void LoadGame()
        {
            _gameController.Load(_game.CurrentLevel.SceneName);
        }
    }
}