using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PresentationLayer.Game.Collectibles
{
    public class ChestCountdownView : MonoBehaviour
    {
        [SerializeField] private Canvas _myCanvas;
        [SerializeField] private Image _countdownFillImage;
        
        private float _expirationTime;
        private Coroutine _countdownCoroutine;

        private void OnEnable()
        {
            Reset();
        }

        public void StartCountdown(float expirationTime)
        {
            _expirationTime = expirationTime;
            Reset();
            _countdownCoroutine = StartCoroutine(Countdown());
        }

        public void StopCountdown()
        {
            if (_countdownCoroutine != null)
            {
                StopCoroutine(_countdownCoroutine);
            }
        }

        public void Hide()
        {
            StopCountdown();
            _myCanvas.enabled = false;
        }
        
        private void Reset()
        {
            _myCanvas.enabled = true;
            _countdownFillImage.fillAmount = 1f;
        }

        private IEnumerator Countdown()
        {
            var elapsedTime = 0f;
            while (_expirationTime > 0)
            {
                elapsedTime += Time.deltaTime;
                _countdownFillImage.fillAmount = 1f - elapsedTime / _expirationTime;
                yield return null;
            }
        }
    }
}