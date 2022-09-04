using UnityEngine;

namespace PresentationLayer.Game.Platforms
{
    public class SpringController : MonoBehaviour
    {
        [SerializeField] private LayerMask _heroLayerMask;
        [SerializeField] private SpringAnimatorController _animatorController;
        
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
            // TODO: set bounce force through scriptable object config
            const float BounceForce = 11f;
            
            otherRigidbody.velocity = new Vector2(otherRigidbody.velocity.x, 0);
            otherRigidbody.AddForce(Vector2.up * BounceForce, ForceMode2D.Impulse);
        }
    }
}
