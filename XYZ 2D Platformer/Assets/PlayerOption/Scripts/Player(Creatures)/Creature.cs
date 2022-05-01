using PlayerOption.Scripts.Components;
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

        [SerializeField] protected LayerMask _graundLayer;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private CheckCircleOverLap _attackRange;

        [Header("Particles")] 
        [SerializeField] protected SpawnListComponent _particles;

        protected Rigidbody2D Rigidbody;
        protected Vector2 Direction;
        protected Animator Animator;
        protected bool IsGrounded;
        private bool _isJumpimg;
        private CapsuleCollider2D _capsuleCollider2D; // --------------------------------------


        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsVerticalVelocityKey = Animator.StringToHash("vertical-velocity");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int Hit = Animator.StringToHash("is-hit");
        private static readonly int AttackKey = Animator.StringToHash("is-attack");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            _capsuleCollider2D = GetComponent<CapsuleCollider2D>(); // --------------------------------------
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
            var xVelocity = Direction.x * _moveSpeed;
            var yVelocity = CalculateYVelocity();
            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);
         
            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetBool(IsRunningKey, Direction.x != 0);
            Animator.SetFloat(IsVerticalVelocityKey, Rigidbody.velocity.y);
         
            Flip(Direction);
        }

        public void ChangeColliderOnDie()                               // --------------------------------------
        {
            _capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
            _capsuleCollider2D.size = new Vector2(0.65f, 0.35f);
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
                yVelocity =isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            else if (Rigidbody.velocity.y > 0 && _isJumpimg)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            if (!IsGrounded) return yVelocity;
            yVelocity += _jumpSpeed;
            _particles.Spawn("Jump");

            return yVelocity;
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
            Animator.SetTrigger(Hit);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
        }
        
        public virtual void Attack()
        {
            Animator.SetTrigger(AttackKey);
        }
        
        public void OnDoAttack()
        {
            _attackRange.Check();
        }
        
    }
}

