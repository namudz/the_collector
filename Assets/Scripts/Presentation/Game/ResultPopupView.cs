using Game;
using Game.Signals;
using Services;
using Services.EventDispatcher;
using Services.SceneLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Game
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

        private void Awake()
        {
            _sceneLoader = ServiceLocator.Instance.GetService<ISceneLoader>();
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
            var iGame = ServiceLocator.Instance.GetService<IGame>();
            iGame.Reset();
            // Delaying the restart of the game so pooled objects can go back to pool
            Invoke(nameof(DelayedStartAgain), Time.deltaTime);
        }

        private void DelayedStartAgain()
        {
            var iGame = ServiceLocator.Instance.GetService<IGame>();
            iGame.Start(true);
        }

        private void BackToHome()
        {
            var dispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            dispatcher.Dispatch(new GameDestroyedSignal());
            
            _sceneLoader.LoadScene(SceneConstants.Scene.MainMenu);
        }
    }
}