using EventDispatcher;
using Game.Signals;
using UnityEngine;

namespace Presentation.Game
{
    public class ResultCanvasView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Canvas _myCanvas;
        [SerializeField] private ResultPopupView _resultPopup;

        private void Awake()
        {
            var eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            eventDispatcher.Subscribe<GameOverSignal>(Show);
            
            Hide();
        }

        private void Hide()
        {
            EnableCanvas(false);
        }

        private void Show(ISignal signal)
        {
            EnableCanvas(true);
            _resultPopup.UpdateData();
        }

        private void EnableCanvas(bool isEnabled)
        {
            _myCanvas.enabled = isEnabled;
        }
    }
}