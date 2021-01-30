using DataPersistence;
using EventDispatcher;
using Game.Level;
using SceneLoader;
using UnityEngine;

namespace InterfaceAdapters.Installers
{
    public class MainInstaller : IMainInstaller
    {
        private ILevelsRepository _levelsRepository;
        private IDataPersistence _dataPersistence;
        private const string LevelConfigsPath = "Levels";

        public void Register()
        {
            ServiceLocator.Instance.RegisterService(this);
        }

        public void InstallDependencies()
        {
            var eventDispatcher = new EventDispatcher.EventDispatcher();
            var sceneLoader = new SceneLoader.SceneLoader(eventDispatcher);
            _levelsRepository = new LevelsRepository();
            _dataPersistence = new PlayerPrefsDataPersistence();
            

            ServiceLocator.Instance.RegisterService<IEventDispatcher>(eventDispatcher);
            ServiceLocator.Instance.RegisterService<ISceneLoader>(sceneLoader);
            ServiceLocator.Instance.RegisterService(_levelsRepository);
            ServiceLocator.Instance.RegisterService(_dataPersistence);
        }

        public void LoadLevels()
        {
            var levelConfigs = Resources.LoadAll<LevelConfig>(LevelConfigsPath);
            foreach (var config in levelConfigs)
            {
                _levelsRepository.AddLevel(config.Level);
            }
        }
    }
}