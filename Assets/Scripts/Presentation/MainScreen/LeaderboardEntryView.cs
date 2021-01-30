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

        private const string DefaultScoreValue = "-";
        private const string DefaultUserName = "---";

        public void SetEntryData(LeaderboardEntry entry)
        {
            _positionText.SetText($"{entry.Position}.");
            _nameText.SetText(string.IsNullOrEmpty(entry.UserName) ? DefaultUserName : entry.UserName);
            _scoreText.SetText(entry.Score > 0 ? entry.Score.ToString() : DefaultScoreValue);
        }
    }
}