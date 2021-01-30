using Game.Level;
using SceneLoader;
using TMPro;
using UnityEngine;

namespace Presentation.MainScreen
{
    public class MainScreenView : MonoBehaviour
    {
        [SerializeField] private LevelEntryView[] _levelEntries;
        [SerializeField] private TextMeshProUGUI _versionText;

        private ISceneLoader _sceneLoader;

        private void Awake()
        {
            SetGameVersion();
        }

        private void Start()
        {
            GetDependencies();
            UpdateLevelEntries();
        }

        private void GetDependencies()
        {
            _sceneLoader = ServiceLocator.Instance.GetService<ISceneLoader>();
        }
        
        private void UpdateLevelEntries()
        {
            var levelsRepository = ServiceLocator.Instance.GetService<ILevelsRepository>();
            for (var i = 0; i < _levelEntries.Length; i++)
            {
                _levelEntries[i].SetLevelData(levelsRepository.GetLevel(i));
            }
        }

        private void PlayGame()
        {
            _sceneLoader.LoadScene(SceneConstants.Scene.Game);
        }

        private void SetGameVersion()
        {
            _versionText.SetText($"v{Application.version}");
        }
    }
}