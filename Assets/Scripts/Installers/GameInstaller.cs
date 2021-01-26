using System.Collections.Generic;

namespace Installers
{
    public class GameInstaller
    {
        public IGame Game { get; private set; }

        private GameInstaller()
        {
            InitializeGame();
        }
        public GameInstaller(ISpawner heroSpawner, IEnumerable<ISpawner> collectibleSpawners): this()
        {
            Game.InjectSpawners(heroSpawner, collectibleSpawners);
        }

        public void LoadGame()
        {
            Game.Load();
        }

        private void InitializeGame()
        {
            var mazeLoader = new MazeLoader(MainInstaller.Instance.SceneLoader);
            Game = new Game(mazeLoader);
        }
    }
}