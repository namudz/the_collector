using InterfaceAdapters.Installers;
using UnityEngine;

namespace Presentation.Installers
{
    public class MainInstallerView : MonoBehaviour
    {
        private void Awake()
        {
            if (ServiceLocator.Instance.ServiceExist<IMainInstaller>() ) { return; }
            
            var installer = new MainInstaller();
            installer.Register();
            installer.InstallDependencies();
            installer.LoadLevels();
        }
    }
}