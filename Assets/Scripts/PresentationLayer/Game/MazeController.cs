using System;
using InterfaceAdapters.Game;
using InterfaceAdapters.Services;
using InterfaceAdapters.Services.EventDispatcher;
using InterfaceAdapters.Signals;
using PresentationLayer.Game.Spawners;
using UnityEngine;

namespace PresentationLayer.Game
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
            _mazeLoader = ServiceLocator.Instance.GetService<IMazeLoader>();
            
            _eventDispatcher.Subscribe<GameResetSignal>(Reset);
            _eventDispatcher.Subscribe<LoadMazeItemsToRestartSignal>(LoadMazeItemsToRestart);
        }

        private void OnDestroy()
        {
            _eventDispatcher.Unsubscribe<GameResetSignal>(Reset);
            _eventDispatcher.Unsubscribe<LoadMazeItemsToRestartSignal>(LoadMazeItemsToRestart);
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