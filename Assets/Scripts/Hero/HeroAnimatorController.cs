﻿using UnityEngine;

public class HeroAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;
    
    private static readonly int IsOnGround = Animator.StringToHash("IsOnGround");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsGrinding = Animator.StringToHash("IsGrinding");
    private static readonly int VelocityX = Animator.StringToHash("VelocityX");
    private static readonly int VelocityY = Animator.StringToHash("VelocityY");

    public void SetIsOnGround(bool isOnGround)
    {
        _animator.SetBool(IsOnGround, isOnGround);
    }

    public void SetIsJumping(bool isJumping) 
    {
        _animator.SetBool(IsJumping, isJumping);
    }

    public void SetIsGrinding(bool isGrinding)
    {
        _animator.SetBool(IsGrinding, isGrinding);
    }
    
    public void SetVelocity(Vector2 velocity)
    {
        _animator.SetFloat(VelocityX, velocity.x);
        _animator.SetFloat(VelocityY, velocity.y);
    }

    public void FlipSprite()
    {
        _sprite.flipX = !_sprite.flipX;
    }

    public void FlipSpriteIfFalling()
    {
        var isFalling = _animator.GetFloat(VelocityY) < 0f;
        if (isFalling)
        {
            FlipSprite();
        }
    }
}
