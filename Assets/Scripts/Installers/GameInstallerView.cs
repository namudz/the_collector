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
            _gameInstaller.LoadGame();
        }

        private void Start()
        {
            Invoke(nameof(FakeDelayedStart), 2);
        }

        private void FakeDelayedStart()
        {
            _gameInstaller.Game.Start();
        }
    }
}