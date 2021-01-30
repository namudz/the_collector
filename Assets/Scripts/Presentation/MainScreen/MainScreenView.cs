using System;
using SceneLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.MainScreen
{
    public class MainScreenView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private TextMeshProUGUI _versionText;

        private ISceneLoader _sceneLoader;

        private void Awake()
        {
            _playButton.onClick.AddListener(PlayGame);
            SetGameVersion();
        }

        private void Start()
        {
            GetDependencies();
        }

        private void GetDependencies()
        {
            _sceneLoader = ServiceLocator.Instance.GetService<ISceneLoader>();
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