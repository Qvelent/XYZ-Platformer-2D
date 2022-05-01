using PlayerOption.Scripts.Components.ColliderBased;
using PlayerOption.Scripts.Components.Health;
using PlayerOption.Scripts.Model;
using PlayerOption.Scripts.Utils;
using UnityEditor.Animations;
using UnityEngine;

namespace PlayerOption.Scripts.Player_Creatures_.Player
{
   public class Player : Creature
   {
      [Header("Player Check")]
      [SerializeField] private LayerCheck _wallCheck;
      [SerializeField] private CheckCircleOverLap _interactionCheck;
      
            
      [SerializeField] private float _slamDownVelocity;
      [SerializeField] private CoolDown _throwCD;

      [Header("(Dis)Armed")]
      [SerializeField] private AnimatorController _armed;
      [SerializeField] private AnimatorController _disarmed;
      
      [Space] [Header("Particles")] [SerializeField]
      private ParticleSystem _hitParticles;
      
      private static readonly int ThrowKey = Animator.StringToHash("throw");
      private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");
      
      private bool _isDoubleJump;
      private bool _isOnWall;
      
      private GameSession _session;
      private float _defaultGravityScale;

      protected override void Awake()
      {
         base.Awake();
         
         _defaultGravityScale = Rigidbody.gravityScale;
      }
      
      private void Start()
      {
         _session = FindObjectOfType<GameSession>();
         var health = GetComponent<HealthComponent>();
         
         health.SetHealth(_session.Data.Hp);
         UpdatePlayerWeapon();
      }
      
      public void OnHealthChange(int currentHealth)
      {
         _session.Data.Hp = currentHealth;
      }
      
      protected override void Update()
      {
         base.Update();

         var moveToSameDirection = Direction.x * transform.lossyScale.x > 0;
         if (_wallCheck.IsTouchingLayer && moveToSameDirection)
         {
            _isOnWall = true;
            Rigidbody.gravityScale = 0;
         }
         else
         {
            _isOnWall = false;
            
            Rigidbody.gravityScale = _defaultGravityScale;
         }
         
         Animator.SetBool(IsOnWall, _isOnWall);
      }
      
      protected override float CalculateYVelocity()
      {
         var isJumpingPressing = Direction.y > 0;

         if (IsGrounded || _isOnWall)
         {
            _isDoubleJump = true;
         }

         if (!isJumpingPressing && _isOnWall) return 0f;
         
         return base.CalculateYVelocity();
      }

      protected override float CalculateJumpVelocity(float yVelocity)
      {
        if (!IsGrounded && _isDoubleJump && !_isOnWall)
        {
           _particles.Spawn("Jump");
           _isDoubleJump = false;
           return _jumpSpeed;
        }

        return base.CalculateJumpVelocity(yVelocity);
      }
      
      public void AddCoins(int coins)
      {
         _session.Data.Coins += coins;
         Debug.Log("Collect coins: " + _session.Data.Coins); // Можно написать $" {coins} Collect coins: {_coins}"
      }

      public void AddSwords(int swords)
      {
         _session.Data.Swords += swords;
         Debug.Log("Collect swords: " + _session.Data.Swords);
      }

      public override void TakeDamage()
      {
         base.TakeDamage();
         
         if (_session.Data.Coins > 0)
         {
            SpawnCoins();
         }
      }

      private void SpawnCoins()
      {
         var numCoinsToDispose = Mathf.Min(_session.Data.Coins, 5);
         _session.Data.Coins -= numCoinsToDispose;
         Debug.Log("Falls coins: " + _session.Data.Coins);
         
         var burst = _hitParticles.emission.GetBurst(0);
         burst.count = numCoinsToDispose;
         _hitParticles.emission.SetBurst(0, burst);
         
         _hitParticles.gameObject.SetActive(true);
         _hitParticles.Play();
      }

      public void Interact()
      {
         _interactionCheck.Check();
      }

      private void OnCollisionEnter2D(Collision2D other)
      {
         if (other.gameObject.IsInLayer(_graundLayer))
         {
            var contact = other.contacts[0];
            if (!(contact.relativeVelocity.y >= _slamDownVelocity)) return;
            _particles.Spawn("SlamDown");
         }
      }

      public void SwordAttack()
      {
         _particles.Spawn("Attack");
      }

      public override void Attack()
      {
         if (!_session.Data.IsArmed) return;
         
         base.Attack();
      }
      
      public void ArmPlayer()
      {
         _session.Data.IsArmed = true;
         UpdatePlayerWeapon();
         Animator.runtimeAnimatorController = _armed;
      }

      private void UpdatePlayerWeapon()
      {
         Animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _disarmed;
      }

      public void OnThrow()
      {
         if (_session.Data.Swords > 1)
         {
            var numSwordsToDispose = Mathf.Min(_session.Data.Swords, 1);
            _session.Data.Swords -= numSwordsToDispose;
            Debug.Log("Falls sword: " + _session.Data.Swords);

            _particles.Spawn("Throw");
         }
      }

      public void Throw()
      {
         if (_throwCD.IsReady && _session.Data.Swords > 1)
         {
            Animator.SetTrigger(ThrowKey);
            _throwCD.Reset();
         }
      }
   }
}

