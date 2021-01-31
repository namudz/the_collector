using Collectibles;
using Collectibles.Spawner;
using Game;
using InterfaceAdapters.Installers;
using UnityEngine;

namespace Presentation.Installers
{
    public class GameInstallerView : MonoBehaviour
    {
        // TODO : install the GameInstaller here. To do so, need to pass the level to play into the load game scene signal
        [SerializeField] private Transform _poolParent;
        //[SerializeField] private GameObjectPoolData[] _collectiblePoolsData;

        [SerializeField] private CollectibleConfig[] _collectibleConfigs;
        
        private IGame _game;

        private void Awake()
        {
            _game = ServiceLocator.Instance.GetService<IGame>();
            var collectiblesSpawner = ServiceLocator.Instance.GetService<ICollectiblesSpawner>();
            collectiblesSpawner.SetAvailableCollectibles(_collectibleConfigs);
            
            
            var gameInstaller = ServiceLocator.Instance.GetService<IGameInstaller>();
            gameInstaller.InitializePools(_poolParent);
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