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
        
        private IEventDispatcher _eventDispatcher;

        private void Awake()
        {
            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            _eventDispatcher.Subscribe<GameOverSignal>(Show);
            _eventDispatcher.Subscribe<GameStartedSignal>(Hide);
            Hide();
        }

        private void OnDestroy()
        {
            _eventDispatcher.Unsubscribe<GameOverSignal>(Show);
            _eventDispatcher.Unsubscribe<GameStartedSignal>(Hide);
        }

        private void Hide(ISignal signal = null)
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