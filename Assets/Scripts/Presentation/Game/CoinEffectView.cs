using Collectibles.Pool;
using Services;
using Services.Pooling;
using TMPro;
using UnityEngine;

namespace Presentation.Game
{
    public class CoinEffectView : MonoBehaviour, IPoolable
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        private IGameObjectPool<CoinEffectView> _pool;

        private const string ScoreFormat = "+{0}";

        private void Awake()
        {
            _pool = ServiceLocator.Instance.GetService<IGameObjectPool<CoinEffectView>>();
        }

        public void Show(int score)
        {
            _scoreText.SetText(string.Format(ScoreFormat, score));
        }

        private void BackToPool()
        {
            gameObject.SetActive(false);
            _pool.BackToPool(gameObject);
        }
    }
}