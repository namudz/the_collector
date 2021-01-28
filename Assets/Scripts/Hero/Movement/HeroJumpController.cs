using InputHandler;
using UnityEngine;

namespace Hero.Movement
{
    public class HeroJumpController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private HeroCollisionsController _collisionsController;
        
        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private HeroMovement _movementController;
        
        [Header("Stats - Retrieve from ScriptableObject")]
        [SerializeField] private float _jumpForce = 6f;
        [SerializeField] private float _jumpForceGrindingMultiplier = 1.1f;
        [SerializeField] private float _jumpDelay = 0.1f;
        [SerializeField] private float _fallMultiplier = 3f;
        [SerializeField] private float _fallMultiplierGrindingWall = 0.33f;
        [SerializeField] private float _gravity = 1f;

        private float _jumpTimer;
        private IInputHandler _inputHandler;
        private const float FallMultiplierHalfFactor = 2f;

        private bool CanJumpGrindingWall => _collisionsController.IsGrindingWall && !_collisionsController.IsOnGround;

        public void InjectDependencies(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _inputHandler.OnTap += UpdateJumpTimer;
        }
        
        private void Update()
        {
            _inputHandler.HandleInput();
        }

        private void FixedUpdate()
        {
            if (_jumpTimer > Time.time)
            {
                TryJump();
            }

            UpdatePhysics();
        }

        private void UpdateJumpTimer()
        {
            _jumpTimer = Time.time + _jumpDelay;
        }

        private void UpdatePhysics()
        {
            _rigidbody.gravityScale = _collisionsController.IsOnGround ? 0f : _gravity;
            if (!_collisionsController.IsOnGround && !_collisionsController.IsGrindingWall)
            {
                var fallMultiplier = _rigidbody.velocity.y < 0 ? _fallMultiplier : _fallMultiplier / FallMultiplierHalfFactor; 
                _rigidbody.gravityScale = _gravity * fallMultiplier;
            }

            if (_collisionsController.IsGrindingWall)
            {
                _rigidbody.gravityScale = _rigidbody.velocity.y > 0 ? _gravity * _fallMultiplier / FallMultiplierHalfFactor : _gravity * _fallMultiplierGrindingWall;
            }
        }

        private void TryJump()
        {
            if (CanJumpGrindingWall)
            {
                _movementController.AccelerateOnJump();
                ExecuteJump();
                return;
            }

            if (_collisionsController.IsOnGround)
            {
                ExecuteJump();
            }
        }
        private void ExecuteJump()
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);

            var jumpMultiplier = CanJumpGrindingWall ? _jumpForceGrindingMultiplier : 1f;
            _rigidbody.AddForce(Vector2.up * (_jumpForce * jumpMultiplier), ForceMode2D.Impulse);
            
            _jumpTimer = 0f;
        }
    }
}