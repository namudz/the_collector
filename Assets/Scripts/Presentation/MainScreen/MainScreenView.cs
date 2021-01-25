using SceneLoader;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.MainScreen
{
    public class MainScreenView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _rankingButton;

        private ISceneLoader _sceneLoader;

        private void Awake()
        {
            _playButton.onClick.AddListener(PlayGame);
            _rankingButton.onClick.AddListener(ShowRankingPopup);
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
    }
}