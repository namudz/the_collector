using System.Collections.Generic;

namespace Installers
{
    public class GameInstaller
    {
        public IGame Game { get; private set; }

        private GameInstaller()
        {
            Game = new Game();
        }
        public GameInstaller(ISpawner heroSpawner, IEnumerable<ISpawner> collectibleSpawners): this()
        {
            Game.InjectSpawners(heroSpawner, collectibleSpawners);
        }

        public void LoadGame()
        {
            Game.Load();
        }
    }
}