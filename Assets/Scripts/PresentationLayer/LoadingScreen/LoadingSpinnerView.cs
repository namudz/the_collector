using UnityEngine;

namespace PresentationLayer.LoadingScreen
{
    public class LoadingSpinnerView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private void OnEnable()
        {
            _animator.Rebind();
        }
    }
}