using System;
using UnityEngine;

namespace Hero
{
    [CreateAssetMenu(fileName = "HeroStats", menuName = "ScriptableObjects/Hero Stats Config", order = 1)]
    public class HeroStatsConfig : ScriptableObject
    {
        public MovementStats MovementStats;
        public JumpStats JumpStats;
        public CollisionConfig CollisionConfig;
    }

    [Serializable]
    public class MovementStats
    {
        public float Speed;
        public float MaxSpeed;
    }
    
    [Serializable]
    public class JumpStats
    {
        public float Force;
        public float ForceGrindingMultiplier;
        public float Delay;
        public float FallMultiplier;
        public float FallMultiplierGrindingWall;
        public float Gravity;
    }
    
    [Serializable]
    public class CollisionConfig
    {
        public Vector3 GroundColliderOffset;
        public Vector3 LateralTopColliderOffset;
        public Vector3 LateralBottomColliderOffset;
        public float RaycastGroundLength;
        public float RaycastLateralLength;
    }
}