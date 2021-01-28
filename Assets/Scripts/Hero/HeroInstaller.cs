using System;
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
        
        private void Awake()
        {
            var inputHandler = CreateHandler();

            _jumpController.InjectDependencies(inputHandler);
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