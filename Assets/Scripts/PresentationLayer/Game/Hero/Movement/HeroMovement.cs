using InterfaceAdapters.Game;
using InterfaceAdapters.Services;
using InterfaceAdapters.Services.EventDispatcher;
using InterfaceAdapters.Signals;
using PresentationLayer.ScriptableObjects;
using UnityEngine;

namespace PresentationLayer.Game.Hero.Movement
{
    public class HeroMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private HeroCollisionsController _collisionsController;
        
        [Header("Stats")]
        [SerializeField] private HeroStatsConfig _config;
        
        public bool IsFalling => _rigidbody.velocity.y < 0;

        private IGame _iGame;
        private IEventDispatcher _eventDispatcher;
        private Vector2 _direction = Vector2.right;

        private bool _isJumpingOnCurrentFrame;
        private bool _isAffectedBySpring;

        private void Awake()
        {
            _iGame = ServiceLocator.Instance.GetService<IGame>();
            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            _eventDispatcher.Subscribe<GameOverSignal>(StopMoving);
            _eventDispatcher.Subscribe<GameResetSignal>(Reset);
        }

        private void OnDestroy()
        {
            _eventDispatcher.Unsubscribe<GameOverSignal>(StopMoving);
            _eventDispatcher.Unsubscribe<GameResetSignal>(Reset);
        }

        private void FixedUpdate()
        {
            if (!_iGame.HasGameStarted || _iGame.IsGameOver) { return; }
            
            Move();
            
            _isAffectedBySpring = _collisionsController.IsAffectedBySpring;
            TryResetIsJumping();
        }
        
        public void AccelerateOnJump()
        {
            _isJumpingOnCurrentFrame = true;
            InvertDirection();
            _rigidbody.AddForce(_direction * _config.MovementStats.SpeedX, ForceMode2D.Impulse);
        }

        public void HandleSpringBounce(bool isVerticalBounce)
        {
            if (isVerticalBounce) { return; }

            _isAffectedBySpring = true;

            if (!_isJumpingOnCurrentFrame)
            {
                InvertDirection();
            }
            
            _collisionsController.HandleSpringBounce(isVerticalBounce);
        }

        private void InvertDirection()
        {
            _direction *= -1;
        }

        private void Move()
        {
            _rigidbody.AddForce(_direction * _config.MovementStats.SpeedX);
            if (Mathf.Abs(_rigidbody.velocity.x) > _config.MovementStats.MaxSpeedX && !_isAffectedBySpring)
            {
                var newSpeedX = Mathf.Sign(_rigidbody.velocity.x) * _config.MovementStats.MaxSpeedX;
                _rigidbody.velocity = new Vector2(newSpeedX, _rigidbody.velocity.y);
            }

            if (_collisionsController.IsGrindingWall 
                && _rigidbody.velocity.y < 0
                && Mathf.Abs(_rigidbody.velocity.y) > _config.MovementStats.MaxSpeedY)
            {
                var newSpeedY = Mathf.Sign(_rigidbody.velocity.y) * _config.MovementStats.MaxSpeedY;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, newSpeedY);
            }
        }
        
        private void StopMoving(ISignal signal = null)
        {
            _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
        }
        
        private void Reset(ISignal _)
        {
            StopMoving();
            _direction = Vector2.right;
            _isJumpingOnCurrentFrame = false;
            _isAffectedBySpring = false;
        }

        private void TryResetIsJumping()
        {
            if (_isJumpingOnCurrentFrame && !_collisionsController.IsGrindingWall && _collisionsController.IsOnGround)
            {
                _isJumpingOnCurrentFrame = false;
            }
        }

        public void RecoverFullSpeed()
        {
            var newHorizontalSpeed = Mathf.Sign(_direction.x) * _config.MovementStats.MaxSpeedX;
            _rigidbody.velocity = new Vector2(newHorizontalSpeed, _rigidbody.velocity.y);
        }
    }
}