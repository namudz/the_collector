using Presentation.MainScreen;
using UnityEngine;

namespace Installers
{
    public class MainInstallerView : MonoBehaviour
    {
        [SerializeField] private MainScreenView _mainScreenView;

        private void Awake()
        {
            var views = new MainViews
            {
                MainScreenView = _mainScreenView
            };
            var installer = new MainInstaller(views);
            installer.InjectDependencies();
        }
    }
}