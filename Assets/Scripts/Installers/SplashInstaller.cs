using System;
using InterfaceAdapters.Game;
using InterfaceAdapters.Game.Level;
using InterfaceAdapters.ScriptableObjects;
using InterfaceAdapters.Services;
using InterfaceAdapters.Services.DataPersistence;
using InterfaceAdapters.Services.EventDispatcher;
using InterfaceAdapters.Services.SceneLoader;
using InterfaceAdapters.UseCases;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Installers
{
    public class SplashInstaller : MonoBehaviour
    {
        [SerializeField] private LevelsConfig _levelConfigs;
        
        private ILevelsRepository _levelsRepository;
        private IDataPersistence _playerPrefsDataPersistence;
        private ISceneLoader _sceneLoader;

        private void Awake()
        {
            InitializeDependencies();

            LoadConfigurations(LoadMainMenu);
        }

        private void InitializeDependencies()
        {
            var eventDispatcher = new EventDispatcher();
            _sceneLoader = new SceneLoader(eventDispatcher, new SceneManagerAdapter());
            _levelsRepository = new LevelsRepository();
            var jsonParser = new JsonUtilityAdapter();
            _playerPrefsDataPersistence = new PlayerPrefsDataPersistence(jsonParser);

            ServiceLocator.Instance.RegisterService<IEventDispatcher>(eventDispatcher);
            ServiceLocator.Instance.RegisterService(_sceneLoader);
            ServiceLocator.Instance.RegisterService(_levelsRepository);
            ServiceLocator.Instance.RegisterService(_playerPrefsDataPersistence);
            ServiceLocator.Instance.RegisterService<IJsonParser>(jsonParser);

            InitializeGame(eventDispatcher);
        }

        private void InitializeGame(IEventDispatcher eventDispatcher)
        {
            // In order to avoid initialization issues when loading levels, the game is created at the splash
            var gameCountdownTimer = new GameCountdownTimer();
            ServiceLocator.Instance.RegisterService<IGameCountdownTimer>(gameCountdownTimer);
            var gameScoreboard = new GameScoreboard();
            ServiceLocator.Instance.RegisterService<IGameScoreboard>(gameScoreboard);
            
            var game = new Game( 
                gameCountdownTimer,
                gameScoreboard,
                eventDispatcher
            );
            ServiceLocator.Instance.RegisterService<IGame>(game);
        }

        private void LoadConfigurations(Action onComplete)
        {
            var loadLevelsRepositoryUseCase = new LoadLevelsRepositoryUseCase(_levelsRepository, _playerPrefsDataPersistence);
            var useCase = new LoadConfigurationsUseCase(loadLevelsRepositoryUseCase);
            
            useCase.Load(_levelConfigs.GetLevels(), onComplete);
        }
        
        private void LoadMainMenu()
        {
            _sceneLoader.LoadScene(SceneConstants.Scene.MainMenu, LoadSceneMode.Single);
        }
    }
}