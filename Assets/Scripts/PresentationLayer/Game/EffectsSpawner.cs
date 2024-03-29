using InterfaceAdapters.Services;
using InterfaceAdapters.Services.EventDispatcher;
using InterfaceAdapters.Services.GameObjectPooling;
using InterfaceAdapters.Signals;
using UnityEngine;

namespace PresentationLayer.Game
{
    public class EffectsSpawner : MonoBehaviour
    {
        private static class FxTypes
        {
            public static string CoinFx = "CoinFx";
        }
        
        [Header("Dependencies")]
        [SerializeField] private GameObjectPoolCollection _poolsCollection;

        private IEventDispatcher _eventDispatcher;

        private void Awake()
        {
            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            
            _poolsCollection.Initialize(true);
            
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _eventDispatcher.Subscribe<ShowCoinFxSignal>(SpawnCoinFx);
        }

        private void UnsubscribeEvents()
        {
            _eventDispatcher.Unsubscribe<ShowCoinFxSignal>(SpawnCoinFx);
        }
        
        private void SpawnCoinFx(ShowCoinFxSignal signal)
        {
            var instance = _poolsCollection.GetInstance(FxTypes.CoinFx, signal.SpawnPosition);
            instance.GetComponent<CoinEffectView>()?.Show(signal.Amount);
        }
    }
}