using InterfaceAdapters.Installers;
using UnityEngine;

namespace Presentation.Installers
{
    public class MainInstallerView : MonoBehaviour
    {
        private void Awake()
        {
            InitializeMainInstaller();
            InitializeGameInstaller();
        }

        private void InitializeMainInstaller()
        {
            if (ServiceLocator.Instance.ServiceExist<IMainInstaller>()) { return; }

            var installer = new MainInstaller();
            installer.Register();
            installer.InstallDependencies();
            installer.LoadLevels();
        }

        private void InitializeGameInstaller()
        {
            if (ServiceLocator.Instance.ServiceExist<IGameInstaller>()) { return; }
            
            var installer = new GameInstaller();
            installer.Register();
            installer.InstallDependencies();
        }
    }
}