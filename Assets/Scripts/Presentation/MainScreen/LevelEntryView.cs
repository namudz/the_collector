﻿using System;
using Game;
using Game.Level;
using SceneLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.MainScreen
{
    public class LevelEntryView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNameText;
        [SerializeField] private LeaderboardEntryView[] _entries;
        [SerializeField] private Button _playButton;
        
        private IGame _iGame;

        private void Awake()
        {
            _playButton.onClick.AddListener(PlayLevel);
        }

        public void SetLevelData(Level level)
        {
            _levelNameText.SetText(level.DisplayName);
            for (var i = 0; i < _entries.Length; i++)
            {
                _entries[i].SetEntryData(i, level.Leaderboard.Entries[i]);
            }
        }
        
        private void PlayLevel()
        {
            var sceneLoader = ServiceLocator.Instance.GetService<ISceneLoader>();
            sceneLoader.LoadScene(SceneConstants.Scene.Game);
        }
    }
}