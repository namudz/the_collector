using EventDispatcher;
using Game;
using Game.Signals;
using UnityEngine;

namespace Hero.Movement
{
    public class HeroMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody;

        [Header("Stats - Retrieve from ScriptableObject")]
        [SerializeField] private float _speed;
        [SerializeField] private float _maxSpeed;

        private IGame _iGame;
        private IEventDispatcher _eventDispatcher;
        private Vector2 _direction = Vector2.right;

        private void Awake()
        {
            _iGame = ServiceLocator.Instance.GetService<IGame>();
            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            _eventDispatcher.Subscribe<GameOverSignal>(StopMoving);
        }

        private void OnDestroy()
        {
            _eventDispatcher.Unsubscribe<GameOverSignal>(StopMoving);
        }

        private void FixedUpdate()
        {
            if (!_iGame.HasGameStarted || _iGame.IsGameOver) { return; }
            
            Move();
        }
        
        public void AccelerateOnJump()
        {
            InvertDirection();
            _rigidbody.AddForce(_direction * _maxSpeed, ForceMode2D.Impulse);
        }

        private void InvertDirection()
        {
            _direction *= -1;
        }

        private void Move()
        {
            _rigidbody.AddForce(_direction * _speed);
            if (Mathf.Abs(_rigidbody.velocity.x) > _maxSpeed)
            {
                var newHorizontalSpeed = Mathf.Sign(_rigidbody.velocity.x) * _maxSpeed;
                _rigidbody.velocity = new Vector2(newHorizontalSpeed, _rigidbody.velocity.y);
            }
        }
        
        private void StopMoving(ISignal signal)
        {
            _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
        }
    }
}