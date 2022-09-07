using InterfaceAdapters.Game.Level;
using UnityEngine;

namespace PresentationLayer.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Platform_Spring_Config", menuName = "ScriptableObjects/Platform Config/Spring", order = 1)]
    public class SpringPlatformConfig : ScriptableObject
    {
        [SerializeField] private PlatformSpringDto _config;

        public float BounceForce => _config.BounceForce;
    }
}