using PresentationLayer.Game.Hero.Movement;
using PresentationLayer.ScriptableObjects;
using UnityEngine;

namespace PresentationLayer.Game.Platforms
{
    public class SpringController : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private SpringPlatformConfig _configDto;
        
        [Header("Dependencies")]
        [SerializeField] private LayerMask _heroLayerMask;
        [SerializeField] private SpringAnimatorController _animatorController;
        
        private Quaternion _myRotation;
        private bool _isVertical;

        private void Awake()
        {
            _myRotation = transform.rotation;
            _isVertical = _myRotation.eulerAngles.z == 0 || _myRotation.eulerAngles.z == 180;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!_animatorController.IsReadyToBounce){ return; }
            
            if ((_heroLayerMask & 1 << other.gameObject.layer) != 0)
            {
                _animatorController.SetBounce();
                
                var otherRigidbody = other.GetComponent<Rigidbody2D>();
                if (otherRigidbody == null) { return; }

                MakeHeroBounce(otherRigidbody);
            }
        }

        private void MakeHeroBounce(Rigidbody2D otherRigidbody)
        {
            var currentVelocity = otherRigidbody.velocity;
            otherRigidbody.velocity = _isVertical
                ? new Vector2(currentVelocity.x, 0)
                : new Vector2(0, currentVelocity.y);

            var heroMovementController = otherRigidbody.GetComponent<HeroMovement>();
            heroMovementController.HandleSpringBounce(_isVertical);

            var force = (Vector2.up * _configDto.BounceForce).Rotate(_myRotation);
            otherRigidbody.AddForce(force, ForceMode2D.Impulse);

            
            
            // TODO: should invert hero movement direction & flip sprite for horizontal bounces
            // Also check if the hero is grinding the wall
        }
    }
}
