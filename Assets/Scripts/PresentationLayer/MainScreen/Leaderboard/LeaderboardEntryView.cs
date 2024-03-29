﻿using InterfaceAdapters.Game.Level;
using TMPro;
using UnityEngine;

namespace PresentationLayer.MainScreen.Leaderboard
{
    public class LeaderboardEntryView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _positionText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private const string DefaultScoreValue = "-";
        private const string DefaultUserName = "---";

        public void SetEntryData(int index, LeaderboardEntry entry)
        {
            _positionText.SetText($"{index + 1}.");
            _nameText.SetText(string.IsNullOrEmpty(entry.UserName) ? DefaultUserName : entry.UserName);
            _scoreText.SetText(entry.Score > 0 ? entry.Score.ToString() : DefaultScoreValue);
        }
    }
}