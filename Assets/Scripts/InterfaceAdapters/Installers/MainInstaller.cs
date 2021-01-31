using EventDispatcher;
using Game.Level;
using SceneLoader;
using Services;
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
            ServiceLocator.Instance.RegisterService<IMainInstaller>(this);
        }

        public void InstallDependencies()
        {
            var eventDispatcher = new EventDispatcher.EventDispatcher();
            var sceneLoader = new SceneLoader.SceneLoader(eventDispatcher);
            _levelsRepository = new LevelsRepository();
            var jsonParser = new JsonUtilityAdapter();
            _dataPersistence = new PlayerPrefsDataPersistence(jsonParser);

            ServiceLocator.Instance.RegisterService<IEventDispatcher>(eventDispatcher);
            ServiceLocator.Instance.RegisterService<ISceneLoader>(sceneLoader);
            ServiceLocator.Instance.RegisterService(_levelsRepository);
            ServiceLocator.Instance.RegisterService(_dataPersistence);
            ServiceLocator.Instance.RegisterService<IJsonParser>(jsonParser);
        }

        public void LoadLevels()
        {
            var levelConfigs = Resources.LoadAll<LevelConfig>(LevelConfigsPath);
            foreach (var config in levelConfigs)
            {
                var leaderboard = _dataPersistence.GetLevelLeaderboard(config.Level.Id);
                if (leaderboard == null)
                {
                    config.Level.InitializeLevelLeaderboard(config.Level.Id);
                }
                else
                {
                    config.Level.Leaderboard = leaderboard;
                }
                _levelsRepository.AddLevel(config.Level);
            }
        }
    }
}