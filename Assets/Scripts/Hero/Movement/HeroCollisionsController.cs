using Game;
using UnityEngine;

namespace Hero.Movement
{
    public class HeroCollisionsController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private HeroMovement _movementController;
        [SerializeField] private HeroAnimatorController _animatorController;
        [SerializeField] private ParticleSystem _particles;
        
        [Header("Collision")]
        [SerializeField] private LayerMask _floorLayerMask;

        [Header("Config")]
        [SerializeField] private HeroStatsConfig _config;

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
            CheckRecoverSpeedAfterGrinding();
            UpdateAnimator();
            UpdateParticles();
        }

        private void CheckRecoverSpeedAfterGrinding()
        {
            if (_wasGrinding && !IsGrindingWall && _movementController.IsFalling)
            {
                _movementController.RecoverFullSpeed();
            }
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
            IsOnGround = Physics2D.Raycast(myPosition + _config.CollisionConfig.GroundColliderOffset, Vector2.down, _config.CollisionConfig.RaycastGroundLength, _floorLayerMask)
                          || Physics2D.Raycast(myPosition - _config.CollisionConfig.GroundColliderOffset, Vector2.down, _config.CollisionConfig.RaycastGroundLength, _floorLayerMask);
        }
        
        private void CheckIsGrindingWall()
        {
            var wasGrinding = IsGrindingWall;
            
            var myPosition = transform.position;
            var isGrindingRight = Physics2D.Raycast(myPosition + _config.CollisionConfig.LateralTopColliderOffset , Vector2.right, _config.CollisionConfig.RaycastLateralLength, _floorLayerMask)
                                  || Physics2D.Raycast(myPosition - _config.CollisionConfig.LateralBottomColliderOffset, Vector2.right, _config.CollisionConfig.RaycastLateralLength, _floorLayerMask);
            
            var isGrindingLeft = Physics2D.Raycast(myPosition + _config.CollisionConfig.LateralTopColliderOffset, Vector2.left, _config.CollisionConfig.RaycastLateralLength, _floorLayerMask)
                                 || Physics2D.Raycast(myPosition - _config.CollisionConfig.LateralBottomColliderOffset, Vector2.left, _config.CollisionConfig.RaycastLateralLength, _floorLayerMask);

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

        private void UpdateParticles()
        {
            if (!_wasOnGround && IsOnGround)
            {
                _particles.Play();
            }
        }
        
        private void OnDrawGizmos()
        {
            // Ground raycast
            Gizmos.color = Color.cyan;
            var myPosition = transform.position;
            Gizmos.DrawLine(myPosition + _config.CollisionConfig.GroundColliderOffset, myPosition + _config.CollisionConfig.GroundColliderOffset + Vector3.down * _config.CollisionConfig.RaycastGroundLength);
            Gizmos.DrawLine(myPosition - _config.CollisionConfig.GroundColliderOffset, myPosition - _config.CollisionConfig.GroundColliderOffset + Vector3.down * _config.CollisionConfig.RaycastGroundLength);
            
            // Lateral raycast
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(myPosition + _config.CollisionConfig.LateralTopColliderOffset, myPosition + _config.CollisionConfig.LateralTopColliderOffset + Vector3.right * _config.CollisionConfig.RaycastLateralLength);
            Gizmos.DrawLine(myPosition + _config.CollisionConfig.LateralTopColliderOffset, myPosition + _config.CollisionConfig.LateralTopColliderOffset + Vector3.left * _config.CollisionConfig.RaycastLateralLength);
            Gizmos.DrawLine(myPosition - _config.CollisionConfig.LateralBottomColliderOffset, myPosition - _config.CollisionConfig.LateralBottomColliderOffset + Vector3.right * _config.CollisionConfig.RaycastLateralLength);
            Gizmos.DrawLine(myPosition - _config.CollisionConfig.LateralBottomColliderOffset, myPosition - _config.CollisionConfig.LateralBottomColliderOffset + Vector3.left * _config.CollisionConfig.RaycastLateralLength);
        }
    }
}