using InterfaceAdapters.Services;
using InterfaceAdapters.Services.EventDispatcher;
using InterfaceAdapters.Signals;
using UnityEngine;

namespace PresentationLayer.Game.GUI
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
            _eventDispatcher.Subscribe<GameReadySignal>(Hide);
            Hide();
        }

        private void OnDestroy()
        {
            _eventDispatcher.Unsubscribe<GameOverSignal>(Show);
            _eventDispatcher.Unsubscribe<GameReadySignal>(Hide);
        }

        private void Hide(ISignal _ = null)
        {
            EnableCanvas(false);
        }

        private void Show(ISignal _)
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