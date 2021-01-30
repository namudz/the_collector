using SceneLoader;
using UnityEngine;

namespace Presentation.Installers
{
    public class GameInstaller : MonoBehaviour
    {
        private void Awake()
        {
            InitializeDependencies();
        }

        private void Start()
        {
            ServiceLocator.Instance.GetService<IGame>().Load();
        }
        
        private static void InitializeDependencies()
        {
            var gameScoreboard = new GameScoreboard();
            ServiceLocator.Instance.RegisterService<IGameScoreboard>(gameScoreboard);

            var mazeLoader = new MazeLoader(ServiceLocator.Instance.GetService<ISceneLoader>());
            var game = new global::Game(mazeLoader);

            ServiceLocator.Instance.RegisterService<IGame>(game);
        }
    }
}