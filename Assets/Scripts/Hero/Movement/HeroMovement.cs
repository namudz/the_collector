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

        private bool _canMoveForward = true;
        private Vector2 _direction = Vector2.right;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                InvertDirection();
            }
        }

        private void FixedUpdate()
        {
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
            if (!_canMoveForward) { return; }
            
            _rigidbody.AddForce(_direction * _speed);
            if (Mathf.Abs(_rigidbody.velocity.x) > _maxSpeed)
            {
                var newHorizontalSpeed = Mathf.Sign(_rigidbody.velocity.x) * _maxSpeed;
                _rigidbody.velocity = new Vector2(newHorizontalSpeed, _rigidbody.velocity.y);
            }
        }
    }
}