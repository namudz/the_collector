using Game;
using InterfaceAdapters.Installers;
using UnityEngine;

namespace Presentation.Installers
{
    public class GameInstallerView : MonoBehaviour
    {
        private IGame _game;

        private void Awake()
        {
            if (!ServiceLocator.Instance.ServiceExist<IGameInstaller>())
            {
                var installer = new GameInstaller();
                installer.Register();
                installer.InstallDependencies();
            }

            _game = ServiceLocator.Instance.GetService<IGame>();
        }

        private void Start()
        {
            _game.Load();
        }

        private void Update()
        {
            _game.Tick();
        }
    }
}