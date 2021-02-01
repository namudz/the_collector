using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Game.Collectibles
{
    public class ChestCountdownView : MonoBehaviour
    {
        [SerializeField] private Canvas _myCanvas;
        [SerializeField] private Image _countdownFillImage;
        
        private float _expirationTime;
        private Coroutine _countdownCoroutine;

        public void StartCountdown(float expirationTime)
        {
            _myCanvas.enabled = true;
            _countdownFillImage.fillAmount = 1f;
            _expirationTime = expirationTime;
            _countdownCoroutine = StartCoroutine(Countdown());
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
    }
}