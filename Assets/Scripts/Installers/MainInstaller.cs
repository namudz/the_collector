using Collectibles.Pool;
using InterfaceAdapters.Installers;
using Services;
using UnityEngine;

namespace Installers
{
    public class MainInstaller : MonoBehaviour
    {
        [SerializeField] private GameObjectPoolDataConfig[] _collectiblePoolsDataConfigs;
        
        private void Awake()
        {
            InitializeGameInstaller();
        }

        private void InitializeGameInstaller()
        {
            if (ServiceLocator.Instance.ServiceExist<IGameInstaller>()) { return; }
            
            var installer = new InterfaceAdapters.Installers.GameInstaller();
            installer.Register();
            installer.LoadCollectibles(_collectiblePoolsDataConfigs);
            installer.InstallDependencies();
        }
    }
}