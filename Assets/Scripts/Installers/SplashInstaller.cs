using System;
using Game.Level;
using InterfaceAdapters.ScriptableObjects;
using InterfaceAdapters.Services.SceneLoader;
using InterfaceAdapters.UseCases;
using Services;
using Services.DataPersistence;
using Services.EventDispatcher;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Installers
{
    public class SplashInstaller : MonoBehaviour
    {
        [SerializeField] private LevelsConfig _levelConfigs;
        
        private ILevelsRepository _levelsRepository;
        private IDataPersistence _dataPersistence;
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
            _dataPersistence = new PlayerPrefsDataPersistence(jsonParser);

            ServiceLocator.Instance.RegisterService<IEventDispatcher>(eventDispatcher);
            ServiceLocator.Instance.RegisterService(_sceneLoader);
            ServiceLocator.Instance.RegisterService(_levelsRepository);
            ServiceLocator.Instance.RegisterService(_dataPersistence);
            ServiceLocator.Instance.RegisterService<IJsonParser>(jsonParser);
        }

        private void LoadConfigurations(Action onComplete)
        {
            var loadLevelsRepositoryUseCase = new LoadLevelsRepositoryUseCase(_levelsRepository, _dataPersistence);
            var useCase = new LoadConfigurationsUseCase(loadLevelsRepositoryUseCase);
            
            useCase.Load(_levelConfigs.GetLevels(), onComplete);
        }
        
        private void LoadMainMenu()
        {
            _sceneLoader.LoadScene(SceneConstants.Scene.MainMenu, LoadSceneMode.Single);
        }
    }
}