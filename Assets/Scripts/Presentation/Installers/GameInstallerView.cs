using UnityEngine;

namespace Installers
{
    public class GameInstallerView : MonoBehaviour
    {
        private GameInstaller _gameInstaller;
        
        private void Awake()
        {
            _gameInstaller = new GameInstaller();
        }

        private void Start()
        {
            _gameInstaller.LoadGame();
        }
    }
}