﻿using Game;
using Hero.Movement;
using InputHandler;
using UnityEngine;

namespace Hero
{
    public class HeroInstaller : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField] private HeroMovement _movementController;
        [SerializeField] private HeroJumpController _jumpController;
        [SerializeField] private HeroCollisionsController _collisionsController;
        [SerializeField] private HeroCollector _collectorController;
        
        private void Awake()
        {
            var inputHandler = CreateHandler();

            _jumpController.InjectDependencies(inputHandler, ServiceLocator.Instance.GetService<IGame>());
            _collectorController.InjectDependencies(ServiceLocator.Instance.GetService<IGameScoreboard>());
        }

        private IInputHandler CreateHandler()
        {
    #if UNITY_EDITOR || UNITY_STANDALONE
            return new InputStandaloneHandler();
    #elif UNITY_ANDROID || UNITY_IOS
            return new InputMobileHandler();
    #endif
        }
    }
    
}