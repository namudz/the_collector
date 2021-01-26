using SceneLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.MainScreen
{
    public class MainScreenView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _rankingButton;
        [SerializeField] private TextMeshProUGUI _versionText;

        private ISceneLoader _sceneLoader;

        private void Awake()
        {
            _playButton.onClick.AddListener(PlayGame);
            _rankingButton.onClick.AddListener(ShowRankingPopup);
            SetGameVersion();
        }

        public void InjectDependencies(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void PlayGame()
        {
            _sceneLoader.LoadScene(SceneConstants.Scene.Game);
        }
        
        private void ShowRankingPopup()
        {
            Debug.LogError("TODO - Ranking System");
        }

        private void SetGameVersion()
        {
            _versionText.SetText($"v{Application.version}");
        }
    }
}