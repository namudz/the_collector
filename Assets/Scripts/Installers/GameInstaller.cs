namespace Installers
{
    public class GameInstaller
    {
        public IGame Game { get; private set; }

        public GameInstaller()
        {
            InitializeGame();
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