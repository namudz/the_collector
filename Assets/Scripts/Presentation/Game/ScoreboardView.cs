using Services;
using TMPro;
using UnityEngine;

namespace Presentation.Game
{
    public class ScoreboardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        private IGameScoreboard _gameScoreboard;

        private void Awake()
        {
            _gameScoreboard = ServiceLocator.Instance.GetService<IGameScoreboard>();
            _gameScoreboard.OnScoreUpdated += UpdateScore;
        }

        private void OnDestroy()
        {
            _gameScoreboard.OnScoreUpdated -= UpdateScore;
        }

        private void UpdateScore(int score)
        {
            _scoreText.SetText(score.ToString());
        }
    }
}