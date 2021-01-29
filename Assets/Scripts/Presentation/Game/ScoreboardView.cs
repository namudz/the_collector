using TMPro;
using UnityEngine;

namespace Presentation.Game
{
    public class ScoreboardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        private void Awake()
        {
            GameScoreboard.Instance.OnScoreUpdated += UpdateScore;
        }

        private void UpdateScore(int score)
        {
            _scoreText.SetText(score.ToString());
        }
    }
}