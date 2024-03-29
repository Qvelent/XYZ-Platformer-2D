﻿using PlayerOption.Scripts.Components.Audio;
using PlayerOption.Scripts.Components.ColliderBased;
using PlayerOption.Scripts.Components.GoBased;
using UnityEngine;

namespace PlayerOption.Scripts.Player_Creatures_
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")] 
        [SerializeField] private bool _invertScale;
        [SerializeField] private float _moveSpeed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damageVelocity;

        [Header("Checkers")]
        [SerializeField] protected LayerMask _graundLayer;
        [SerializeField] private ColliderCheck _groundCheck;
        [SerializeField] private CheckCircleOverLap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;

        protected Rigidbody2D Rigidbody;
        protected Vector2 Direction;
        protected Animator Animator;
        protected PlaySoundsComponent Sounds;
        protected bool IsGrounded;
        private bool _isJumpimg;
        
        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsVerticalVelocityKey = Animator.StringToHash("vertical-velocity");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int Hit = Animator.StringToHash("is-hit");
        private static readonly int AttackKey = Animator.StringToHash("is-attack");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoundsComponent>();
        }
        
        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        protected virtual void Update()
        {
           IsGrounded = _groundCheck.IsTouchingLayer;
        }

        private void FixedUpdate()
        {
            var xVelocity = CalculateXVelocity();
            var yVelocity = CalculateYVelocity();
            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);
            
            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetBool(IsRunningKey, Direction.x != 0);
            Animator.SetFloat(IsVerticalVelocityKey, Rigidbody.velocity.y);
            
            Flip(Direction);
        }

        protected virtual float CalculateXVelocity()
        {
            return Direction.x * _moveSpeed;
        }

        protected  virtual float CalculateYVelocity()
        {
            var yVelocity = Rigidbody.velocity.y;
            var isJumpingPressing = Direction.y > 0;

            if (IsGrounded)
            {
                _isJumpimg = false;
            }

            if (isJumpingPressing)
            {
                _isJumpimg = true;
                
                var isFalling = Rigidbody.velocity.y <= 0.001f;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            else if (Rigidbody.velocity.y > 0 && _isJumpimg)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            if (IsGrounded)
            {
                yVelocity = _jumpSpeed;
                DoJumpVfx();
            }

            return yVelocity;
        }

        protected void DoJumpVfx()
        {
            _particles.Spawn("Jump");
            Sounds.Play("Jump");
        }
        
        public void Flip(Vector2 direction)
        {
            var multiplier = _invertScale ? -1 : 1;
            
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier,1,1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3( -1 * multiplier, 1, 1);
            }
        }
        
        public virtual void TakeDamage()
        {
            _isJumpimg = false;
            Animator.SetTrigger(Hit);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
        }
        
        public virtual void Attack()
        {
            Animator.SetTrigger(AttackKey);
            Sounds.Play("Melee");
        }
        
        public void OnDoAttack()
        {
            _attackRange.Check();
        }
    }
}

