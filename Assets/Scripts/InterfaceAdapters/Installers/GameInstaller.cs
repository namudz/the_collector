using Collectibles.Controllers;
using Collectibles.Pool;
using Collectibles.Spawner;
using Game;
using InputHandler;
using Presentation.Game;
using Services;
using Services.EventDispatcher;
using Services.Pooling;
using Services.SceneLoader;
using UnityEngine;
using Collectible = Collectibles.Config.Collectible;

namespace InterfaceAdapters.Installers
{
    public class GameInstaller : IGameInstaller
    {
        private Transform _poolsParent;
        private IGameObjectPool<ChestCollectible> _chestPool;
        private IGameObjectPool<CoinCollectible> _coinPool;
        private IGameObjectPool<CoinEffectView> _coinEffectPool;

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

                InitializePool(poolDataConfig);
            }
        }

        public void InitializePools(Transform poolParent)
        {
            _poolsParent = poolParent;
            _coinPool.InstantiateInitialElements(_poolsParent);
            _chestPool.InstantiateInitialElements(_poolsParent);
            _coinEffectPool.InstantiateInitialElements(_poolsParent);
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
        
        private void InitializePool(GameObjectPoolDataConfig poolDataConfig)
        {
            var iCollectible = poolDataConfig.PoolData.Prefab.GetComponent<ICollectible>();
            if (iCollectible != null)
            {
                switch (iCollectible.Type)
                {
                    case Collectible.CollectibleType.Coin:
                        _coinPool = new CoinPool(poolDataConfig.PoolData);
                        ServiceLocator.Instance.RegisterService(_coinPool);
                        break;

                    case Collectible.CollectibleType.Chest:
                        _chestPool = new ChestPool(poolDataConfig.PoolData);
                        ServiceLocator.Instance.RegisterService(_chestPool);
                        break;
                }

                return;
            }

            var coinEffectView = poolDataConfig.PoolData.Prefab.GetComponent<CoinEffectView>();
            if (coinEffectView != null)
            {
                _coinEffectPool = new CoinEffectPool(poolDataConfig.PoolData);
                ServiceLocator.Instance.RegisterService(_coinEffectPool);
            }
        }
    }
}