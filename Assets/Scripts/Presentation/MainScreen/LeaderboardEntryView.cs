using System;
using Game.Level;
using TMPro;
using UnityEngine;

namespace Presentation.MainScreen
{
    public class LeaderboardEntryView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _positionText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void SetEntryData(LeaderboardEntry entry)
        {
            _positionText.SetText($"{entry.Position}.");
            _nameText.SetText(entry.UserName);
            _scoreText.SetText(entry.Score.ToString());
        }
    }
}