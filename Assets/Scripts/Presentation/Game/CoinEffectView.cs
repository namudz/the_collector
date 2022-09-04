using Services.GameObjectPooling;
using TMPro;
using UnityEngine;

namespace Presentation.Game
{
    public class CoinEffectView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private PoolableGameObject _poolableController;

        private const string ScoreFormat = "+{0}";


        public void Show(int score)
        {
            _scoreText.SetText(string.Format(ScoreFormat, score));
        }

        // Called from Unity Animation
        private void AnimationBackToPool()
        {
            _poolableController.BackToPool();
        }
    }
}