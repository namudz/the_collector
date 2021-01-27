using UnityEngine;

namespace Hero.Movement
{
    public class HeroJumpController : MonoBehaviour
    {
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
        
        
        [Header("Raycasts")]
        [SerializeField] private float _raycastGroundLength = 0.3f;
        [SerializeField] private float _raycastLateralLength = 0.2f;
        
        [Header("Collision")]
        [SerializeField] private LayerMask _floorLayerMask;

        [Header("Ground Collision")]
        [SerializeField] private Vector3 _groundColliderOffset;
        [SerializeField] private Vector3 _lateralTopColliderOffset;
        [SerializeField] private Vector3 _lateralBottomColliderOffset;

        public bool _isOnGround;
        public bool _isGrindingWall;
        private float _jumpTimer;
        private const float FallMultiplierHalfFactor = 2f;

        private bool CanJumpGrindingWall => _isGrindingWall && !_isOnGround;

        private void Update()
        {
            CheckCanJump();
            if (Input.GetKeyDown(KeyCode.W))
            {
                _jumpTimer = Time.time + _jumpDelay;
            }
        }

        private void FixedUpdate()
        {
            if (_jumpTimer > Time.time)
            {
                TryJump();
            }

            UpdatePhysics();
            
            Debug.DrawLine(transform.position, transform.position + (Vector3.up * 0.1f), Color.yellow, 2f);
        }

        private void UpdatePhysics()
        {
            _rigidbody.gravityScale = _isOnGround ? 0f : _gravity;
            if (!_isOnGround && !_isGrindingWall)
            {
                var fallMultiplier = _rigidbody.velocity.y < 0 ? _fallMultiplier : _fallMultiplier / FallMultiplierHalfFactor; 
                _rigidbody.gravityScale = _gravity * fallMultiplier;
            }

            if (_isGrindingWall)
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

            if (_isOnGround)
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
        
        private void CheckCanJump()
        {
            CheckIsGrounded();
            CheckIsGrindingWall();
        }

        private void CheckIsGrounded()
        {
            var myPosition = transform.position;
            _isOnGround = Physics2D.Raycast(myPosition + _groundColliderOffset, Vector2.down, _raycastGroundLength, _floorLayerMask)
                       || Physics2D.Raycast(myPosition - _groundColliderOffset, Vector2.down, _raycastGroundLength, _floorLayerMask);
        }

        private void CheckIsGrindingWall()
        {
            var myPosition = transform.position;
            var isGrindingRight = Physics2D.Raycast(myPosition + _lateralTopColliderOffset , Vector2.right, _raycastLateralLength, _floorLayerMask)
                              || Physics2D.Raycast(myPosition - _lateralBottomColliderOffset, Vector2.right, _raycastLateralLength, _floorLayerMask);
            
            var isGrindingLeft = Physics2D.Raycast(myPosition + _lateralTopColliderOffset, Vector2.left, _raycastLateralLength, _floorLayerMask)
                                 || Physics2D.Raycast(myPosition - _lateralBottomColliderOffset, Vector2.left,
                                     _raycastLateralLength, _floorLayerMask);

            _isGrindingWall = isGrindingRight || isGrindingLeft;
        }
        
        private void OnDrawGizmos()
        {
            // Ground raycast
            Gizmos.color = Color.cyan;
            var myPosition = transform.position;
            Gizmos.DrawLine(myPosition + _groundColliderOffset, myPosition + _groundColliderOffset + Vector3.down * _raycastGroundLength);
            Gizmos.DrawLine(myPosition - _groundColliderOffset, myPosition - _groundColliderOffset + Vector3.down * _raycastGroundLength);
            
            // Lateral raycast
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(myPosition + _lateralTopColliderOffset, myPosition + _lateralTopColliderOffset + Vector3.right * _raycastLateralLength);
            Gizmos.DrawLine(myPosition + _lateralTopColliderOffset, myPosition + _lateralTopColliderOffset + Vector3.left * _raycastLateralLength);
            Gizmos.DrawLine(myPosition - _lateralBottomColliderOffset, myPosition - _lateralBottomColliderOffset + Vector3.right * _raycastLateralLength);
            Gizmos.DrawLine(myPosition - _lateralBottomColliderOffset, myPosition - _lateralBottomColliderOffset + Vector3.left * _raycastLateralLength);
        }
    }
}