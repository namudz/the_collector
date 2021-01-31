using Collectibles;
using Collectibles.Controllers;
using Collectibles.Pool;
using Collectibles.Spawner;
using EventDispatcher;
using Game;
using InputHandler;
using SceneLoader;
using UnityEngine;
using Collectible = Collectibles.Collectible;

namespace InterfaceAdapters.Installers
{
    public class GameInstaller : IGameInstaller
    {
        private Transform _poolsParent;
        private ChestPool _chestPool;
        private CoinPool _coinPool;

        public void Register()
        {
            ServiceLocator.Instance.RegisterService<IGameInstaller>(this);
        }

        public void InstallDependencies()
        {
            var eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            
            var gameScoreboard = new GameScoreboard();
            ServiceLocator.Instance.RegisterService<IGameScoreboard>(gameScoreboard);
            
            var gameCountdownTimer = new GameCountdownTimer();
            ServiceLocator.Instance.RegisterService<IGameCountdownTimer>(gameCountdownTimer);

            var collectiblesSpawner = new CollectiblesSpawner(_coinPool,_chestPool);
            
            var mazeLoader = new MazeLoader(ServiceLocator.Instance.GetService<ISceneLoader>(), collectiblesSpawner);
            
            var game = new global::Game.Game(
                mazeLoader, 
                gameCountdownTimer,
                gameScoreboard,
                eventDispatcher
            );
            ServiceLocator.Instance.RegisterService<IGame>(game);

            InstallInputHandler();
        }

        public void LoadCollectibles(GameObjectPoolDataConfig[] poolsData)
        {
            foreach (var poolDataConfig in poolsData)
            {
                poolDataConfig.PoolData.RootTransform = _poolsParent;
                var controller = poolDataConfig.PoolData.Prefab.GetComponent<ICollectible>();
                switch (controller.Type)
                {
                    case Collectible.CollectibleType.Coin:
                        _coinPool = new CoinPool(poolDataConfig.PoolData);
                        break;
                    
                    case Collectible.CollectibleType.Chest:
                        _chestPool = new ChestPool(poolDataConfig.PoolData);
                        break;
                }
            }
        }
        
        public void InitializePools(Transform poolParent)
        {
            _poolsParent = poolParent;
            _coinPool.InstantiateInitialElements(_poolsParent);
            _chestPool.InstantiateInitialElements(_poolsParent);
        }
        
        private void InstallInputHandler()
        {
            IInputHandler handler;
#if UNITY_EDITOR || UNITY_STANDALONE
            handler = new InputStandaloneHandler();
#elif UNITY_ANDROID || UNITY_IOS
            handler = new InputMobileHandler();
#endif
            ServiceLocator.Instance.RegisterService(handler);
        }
    }
}