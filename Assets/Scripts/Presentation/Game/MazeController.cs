using System;
using Game;
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
    }
}