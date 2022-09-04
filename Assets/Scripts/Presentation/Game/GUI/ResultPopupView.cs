using Game;
using Game.Signals;
using InterfaceAdapters.Services.SceneLoader;
using InterfaceAdapters.UseCases;
using Services;
using Services.EventDispatcher;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Presentation.Game.GUI
{
    public class ResultPopupView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _saveScoreButton;
        [SerializeField] private SaveScorePopupView _saveScoreView;
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button _homeButton;
        
        private IGameScoreboard _gameScoreboard;
        private ISceneLoader _sceneLoader;
        private IRestartGameUseCase _restartGameUseCase;

        private void Awake()
        {
            _sceneLoader = ServiceLocator.Instance.GetService<ISceneLoader>();

            _restartGameUseCase = new RestartGameUseCase(
                ServiceLocator.Instance.GetService<IEventDispatcher>(),
                ServiceLocator.Instance.GetService<IGame>()
            );
        }

        private void Start()
        {
            _gameScoreboard = ServiceLocator.Instance.GetService<IGameScoreboard>();
            _saveScoreButton.onClick.AddListener(ShowSaveScorePopup);
            _playAgainButton.onClick.AddListener(PlayAgain);
            _homeButton.onClick.AddListener(BackToHome);
        }

        public void UpdateData()
        {
            _scoreText.SetText(_gameScoreboard.CurrentScore.ToString());
        }
        
        private void ShowSaveScorePopup()
        {
            _saveScoreView.Show();
        }

        private void PlayAgain()
        {
            _restartGameUseCase.RestartGame();
        }

        private void BackToHome()
        {
            var dispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            dispatcher.Dispatch(new GameDestroyedSignal());
            
            _sceneLoader.LoadScene(SceneConstants.Scene.MainMenu, LoadSceneMode.Single);
        }
    }
}