﻿using InterfaceAdapters.Game;
using InterfaceAdapters.Services;
using InterfaceAdapters.Services.EventDispatcher;
using InterfaceAdapters.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace PresentationLayer.Game.GUI
{
    public class TapToStartView : MonoBehaviour
    {
        [SerializeField] private Canvas _myCanvas;
        [SerializeField] private Button _button;

        private IEventDispatcher _eventDispatcher;
        private IGame _iGame;

        private void Awake()
        {
            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            _iGame = ServiceLocator.Instance.GetService<IGame>(); 
            
            _eventDispatcher.Subscribe<GameReadySignal>(Show);
            _button.onClick.AddListener(StartGame);
        }

        private void OnDestroy()
        {
            _eventDispatcher.Unsubscribe<GameReadySignal>(Show);
        }

        private void Show(ISignal _)
        {
            EnableCanvas(true);
        }

        private void StartGame()
        {
            EnableCanvas(false);
            _iGame.Start();
        }

        private void EnableCanvas(bool isVisible)
        {
            _myCanvas.enabled = isVisible;
        }
    }
}