using Game;
using UnityEngine;

namespace Hero.Movement
{
    public class HeroCollisionsController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private HeroAnimatorController _animatorController;
        
        [Header("Collision")]
        [SerializeField] private LayerMask _floorLayerMask;

        [Header("Ground Collision")]
        [SerializeField] private Vector3 _groundColliderOffset;
        [SerializeField] private Vector3 _lateralTopColliderOffset;
        [SerializeField] private Vector3 _lateralBottomColliderOffset;
        
        [Header("Raycasts")]
        [SerializeField] private float _raycastGroundLength = 0.3f;
        [SerializeField] private float _raycastLateralLength = 0.22f;

        public bool IsOnGround { get; private set; }
        public bool IsGrindingWall  { get; private set; }

        private Color _defaultPositionColor = Color.yellow;
        private Color _collidingColor = Color.red;
        private IGame _iGame;

        private bool _wasOnGround;
        private bool _wasGrinding;

        private void Awake()
        {
            _iGame = ServiceLocator.Instance.GetService<IGame>();
        }

        private void Update()
        {
            if (!_iGame.HasGameStarted || _iGame.IsGameOver) { return; }

            _wasOnGround = IsOnGround;
            _wasGrinding = IsGrindingWall;
            
            CheckIsGrounded();
            CheckIsGrindingWall();
            UpdateAnimator();
        }

        private void FixedUpdate()
        {
            if (!_iGame.HasGameStarted || _iGame.IsGameOver) { return; }
            
            var color = IsOnGround || IsGrindingWall ? _collidingColor : _defaultPositionColor;
            var myPosition = transform.position;
            Debug.DrawLine(myPosition, myPosition + Vector3.up * 0.1f, color, 2f);
        }

        private void CheckIsGrounded()
        {
            var myPosition = transform.position;
            IsOnGround = Physics2D.Raycast(myPosition + _groundColliderOffset, Vector2.down, _raycastGroundLength, _floorLayerMask)
                          || Physics2D.Raycast(myPosition - _groundColliderOffset, Vector2.down, _raycastGroundLength, _floorLayerMask);
        }
        
        private void CheckIsGrindingWall()
        {
            var wasGrinding = IsGrindingWall;
            
            var myPosition = transform.position;
            var isGrindingRight = Physics2D.Raycast(myPosition + _lateralTopColliderOffset , Vector2.right, _raycastLateralLength, _floorLayerMask)
                                  || Physics2D.Raycast(myPosition - _lateralBottomColliderOffset, Vector2.right, _raycastLateralLength, _floorLayerMask);
            
            var isGrindingLeft = Physics2D.Raycast(myPosition + _lateralTopColliderOffset, Vector2.left, _raycastLateralLength, _floorLayerMask)
                                 || Physics2D.Raycast(myPosition - _lateralBottomColliderOffset, Vector2.left, _raycastLateralLength, _floorLayerMask);

            IsGrindingWall = isGrindingRight || isGrindingLeft;

            if (!wasGrinding && IsGrindingWall)
            {
                _animatorController.FlipSprite();
            }
        }

        private void UpdateAnimator()
        {
            _animatorController.SetIsOnGround(IsOnGround);
            _animatorController.SetIsJumping(!IsOnGround);
            _animatorController.SetIsGrinding(IsGrindingWall);

            if (!_wasOnGround && _wasGrinding && !IsGrindingWall)
            {
                _animatorController.FlipSpriteIfFalling();
            }
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