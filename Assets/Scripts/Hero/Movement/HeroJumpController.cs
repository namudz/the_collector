using Game;
using InputHandler;
using UnityEngine;

namespace Hero.Movement
{
    public class HeroJumpController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private HeroCollisionsController _collisionsController;
        [SerializeField] private HeroMovement _movementController;
        [SerializeField] private HeroAnimatorController _animatorController;
        
        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody;
        
        [Header("Stats")] 
        [SerializeField] private HeroStatsConfig _config;

        private IInputHandler _inputHandler;
        private IGame _iGame;
        
        private float _jumpTimer;
        private const float FallMultiplierHalfFactor = 2f;

        private bool CanJumpGrindingWall => _collisionsController.IsGrindingWall && !_collisionsController.IsOnGround;

        private void Awake()
        {
            _inputHandler = ServiceLocator.Instance.GetService<IInputHandler>();
            _iGame = ServiceLocator.Instance.GetService<IGame>();
            _inputHandler.OnTap += UpdateJumpTimer;
        }
        
        private void Update()
        {
            if (_iGame.HasGameStarted && !_iGame.IsGameOver)
            {
                _inputHandler.HandleInput();
            }
        }

        private void FixedUpdate()
        {
            if (!_iGame.HasGameStarted || _iGame.IsGameOver) { return; }
            
            if (_jumpTimer > Time.time)
            {
                TryJump();
            }

            UpdatePhysics();
            _animatorController.SetVelocity(_rigidbody.velocity);
        }

        private void UpdateJumpTimer()
        {
            _jumpTimer = Time.time + _config.JumpStats.Delay;
        }

        private void UpdatePhysics()
        {
            _rigidbody.gravityScale = _collisionsController.IsOnGround ? 0f : _config.JumpStats.Gravity;
            if (!_collisionsController.IsOnGround && !_collisionsController.IsGrindingWall)
            {
                var fallMultiplier = _rigidbody.velocity.y < 0 
                    ? _config.JumpStats.FallMultiplier 
                    : _config.JumpStats.FallMultiplier / FallMultiplierHalfFactor; 
                _rigidbody.gravityScale = _config.JumpStats.Gravity * fallMultiplier;
            }

            if (_collisionsController.IsGrindingWall)
            {
                _rigidbody.gravityScale = _rigidbody.velocity.y > 0 
                    ? _config.JumpStats.Gravity * _config.JumpStats.FallMultiplier / FallMultiplierHalfFactor 
                    : _config.JumpStats.Gravity * _config.JumpStats.FallMultiplierGrindingWall;
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

            var jumpMultiplier = CanJumpGrindingWall ? _config.JumpStats.ForceGrindingMultiplier : 1f;
            _rigidbody.AddForce(Vector2.up * (_config.JumpStats.Force * jumpMultiplier), ForceMode2D.Impulse);
            
            _jumpTimer = 0f;
        }
    }
}