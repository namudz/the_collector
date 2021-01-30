﻿using Game;
using SceneLoader;
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
        }

        private void BackToHome()
        {
            _sceneLoader.LoadScene(SceneConstants.Scene.MainMenu);
        }
    }
}