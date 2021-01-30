using TMPro;
using UnityEngine;

namespace Presentation.Game
{
    public class ScoreboardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        private void Awake()
        {
            var gameScoreboard = ServiceLocator.Instance.GetService<IGameScoreboard>();
            gameScoreboard.OnScoreUpdated += UpdateScore;
        }

        private void UpdateScore(int score)
        {
            _scoreText.SetText(score.ToString());
        }
    }
}