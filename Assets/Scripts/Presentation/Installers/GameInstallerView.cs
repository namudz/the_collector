using System.Collections.Generic;
using UnityEngine;

namespace Installers
{
    public class GameInstallerView : MonoBehaviour
    {
        [SerializeField] private HeroSpawner _heroSpawner;
        // TODO - CollectibleSpawner
        //[SerializeField] private IEnumerable<ISpawner> _collectibleSpawners;
        private GameInstaller _gameInstaller;
        
        private void Awake()
        {
            // TODO - Fill collectible spawners
            _gameInstaller = new GameInstaller(_heroSpawner, new List<ISpawner>());
        }

        private void Start()
        {
            _gameInstaller.LoadGame();
        }
    }
}