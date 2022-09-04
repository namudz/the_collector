using System;
using Game;
using Game.Signals;
using Services;
using Services.EventDispatcher;
using UnityEngine;

namespace Presentation.Game
{
    public class MazeController : MonoBehaviour
    {
        private const string TagHeroSpawner = "HeroSpawner";
        
        [SerializeField] private CollectiblesRenderer _collectiblesRenderer;
        
        private IMazeLoader _mazeLoader;
        private ISpawner _heroSpawner;
        private Action _onMazeFullyLoaded;
        private IEventDispatcher _eventDispatcher;

        private void Awake()
        {
            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            
            _eventDispatcher.Subscribe<GameResetSignal>(Reset);
            _eventDispatcher.Subscribe<LoadMazeItemsToRestartSignal>(LoadMazeItemsToRestart);
        }

        private void OnDestroy()
        {
            _eventDispatcher.Unsubscribe<GameResetSignal>(Reset);
            _eventDispatcher.Unsubscribe<LoadMazeItemsToRestartSignal>(LoadMazeItemsToRestart);
        }

        public void InjectDependencies(IMazeLoader mazeLoader)
        {
            _mazeLoader = mazeLoader;
        }

        public void LoadMaze(string mazeSceneName, Action onMazeFullyLoaded)
        {
            _onMazeFullyLoaded = onMazeFullyLoaded;
            _mazeLoader.Load(mazeSceneName, HandleMazeSceneLoaded);
        }
        
        private void HandleMazeSceneLoaded()
        {
            _collectiblesRenderer.LoadSpawnPoints();
            _heroSpawner = GameObject.FindWithTag(TagHeroSpawner)?.GetComponent<ISpawner>();

            SpawnElements();
        }
        
        private void SpawnElements()
        {
            _collectiblesRenderer.SpawnCollectibles();
            _heroSpawner.Spawn();
            
            _onMazeFullyLoaded?.Invoke();
        }
        
        private void Reset(ISignal _)
        {
            _heroSpawner.Reset();
        }
        
        private void LoadMazeItemsToRestart(ISignal _)
        {
            SpawnElements();
        }
    }
}