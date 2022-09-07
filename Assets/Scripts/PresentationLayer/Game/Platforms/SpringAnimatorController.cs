using UnityEngine;

namespace PresentationLayer.Game.Platforms
{
    public class SpringAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int Bounce = Animator.StringToHash("Bounce");

        public bool IsReadyToBounce { get; private set; } = true;
        
        public void SetBounce()
        {
            _animator.SetTrigger(Bounce);
            IsReadyToBounce = false;
        }

        private void BounceAnimationFinished()
        {
            IsReadyToBounce = true;
        }
    }
}