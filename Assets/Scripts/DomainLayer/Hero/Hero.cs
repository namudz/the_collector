using System;

namespace DomainLayer.Hero
{
    public class Hero
    {
        public MovementStats MovementStats;
        public JumpStats JumpStats;
        public int MaxHp;
    }
    
    [Serializable]
    public class MovementStats
    {
        public float SpeedX;
        public float MaxSpeedX;
        public float MaxSpeedY;
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
}