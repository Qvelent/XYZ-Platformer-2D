using System.Collections;
using PlayerOption.Scripts.Components.ColliderBased;
using PlayerOption.Scripts.Components.GoBased;
using PlayerOption.Scripts.Components.Health;
using PlayerOption.Scripts.Model;
using PlayerOption.Scripts.Model.Definitions;
using PlayerOption.Scripts.Utils;
using UnityEditor.Animations;
using UnityEngine;

namespace PlayerOption.Scripts.Player_Creatures_.Player
{
   public class Player : Creature
   {
      [SerializeField] private ColliderCheck _wallCheck;
      [SerializeField] private CheckCircleOverLap _interactionCheck;
      
      [SerializeField] private float _slamDownVelocity;
      [SerializeField] private CoolDown _throwCD;
      [SerializeField] private AnimatorController _armed;
      [SerializeField] private AnimatorController _disarmed;
      
      [SerializeField] private CoolDown _superThrowCD;
      [SerializeField] private int _superThrowParticles;
      [SerializeField] private float _superThrowDelay;
      [SerializeField] private SpawnComponent _throwSpawner;

      [SerializeField] private CoolDown _dashCD;
      [SerializeField] private float _dashForce;

      [SerializeField] private ProbabilityDropComponent _hitDrop;
      
      private SpriteRenderer _playerSr;
      private static readonly int ThrowKey = Animator.StringToHash("throw");
      private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");
      
      private bool _isDoubleJump;
      private bool _isOnWall;
      private bool _isSuperThrow;

      private GameSession _session;
      private HealthComponent _health;
      private PauseMenuController _pauseMenu;
      private float _defaultGravityScale;

      private const string SwordId = "Sword";
      private int CoinsCount => _session.Data.Inventory.Count("Coin"); // property c# изучить
      private int SwordCount => _session.Data.Inventory.Count(SwordId); // property c# изучить
      private int PotionCount => _session.Data.Inventory.Count("Potion(100)"); // property c# изучить

      private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;

      private bool CanThrow
      {
         get
         {
            if (SelectedItemId == SwordId)
               return SwordCount > 1;

            var def = DefsFacade.I.Items.Get(_session.QuickInventory.SelectedItem.Id);
            return def.HasTag(ItemTag.Throwable);
         }
      }
      


      protected override void Awake()
      {
         base.Awake();
         
         _defaultGravityScale = Rigidbody.gravityScale;
      }
      
      private void Start()
      {
         _session = FindObjectOfType<GameSession>();
         _health = GetComponent<HealthComponent>();
         _pauseMenu = GetComponent<PauseMenuController>();
         _session.Data.Inventory.OnChanged += OnInventoryChanged;
         _session.Data.Inventory.OnChanged += AnotherChanged;
         
         _health.SetHealth(_session.Data.Hp.Value);
         UpdatePlayerWeapon();
      }

      private void OnDestroy()
      {
         _session.Data.Inventory.OnChanged -= OnInventoryChanged;
         _session.Data.Inventory.OnChanged -= AnotherChanged;
      }

      private void AnotherChanged(string id, int value)
      {
         Debug.Log($"Inventory changed: {id}: {value}");
      }

      private void OnInventoryChanged(string id, int value)
      {
         if (id == SwordId)
         {
            UpdatePlayerWeapon();
         }
      }

      public void OnHealthChange(int currentHealth)
      {
         _session.Data.Hp.Value = currentHealth;
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

      protected override float CalculateXVelocity()
      {
         var modifier = _isDashing ? 10 : 1;
         return base.CalculateXVelocity() * modifier;
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
           _isDoubleJump = false;
           DoJumpVfx();
           return _jumpSpeed;
        }

        return base.CalculateJumpVelocity(yVelocity);
      }

      public void AddInInventory(string id, int value)
      {
         _session.Data.Inventory.Add(id, value);
      }

      public override void TakeDamage()
      {
         base.TakeDamage();
         if (CoinsCount <= 0) return;
         SpawnCoins();
      }

      private void SpawnCoins()
      {
         var numCoinsToDispose = Mathf.Min(CoinsCount, 5);
         _session.Data.Inventory.Remove("Coin", numCoinsToDispose);

         _hitDrop.SetCount(numCoinsToDispose);
         _hitDrop.CalculateDrop();
      }

      public void Interact()
      {
         _interactionCheck.Check();
      }

      private void OnCollisionEnter2D(Collision2D other)
      {
         if (!other.gameObject.IsInLayer(_graundLayer)) return;
         var contact = other.contacts[0];
         if (!(contact.relativeVelocity.y >= _slamDownVelocity)) return;
         _particles.Spawn("SlamDown");
      }

      public void SwordAttack()
      {
         _particles.Spawn("Attack");
      }

      
      public override void Attack()
      {
         if (SwordCount <= 0) return;
         
         base.Attack();
      }

      private void UpdatePlayerWeapon()
      {
         Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;
      }

      public void OnThrow()
      {
         if (_isSuperThrow)
         {
            var throwableCount = _session.Data.Inventory.Count(SelectedItemId);
            var possibleCount = SelectedItemId == SwordId ? throwableCount - 1 : throwableCount;
            
            var numThrows = Mathf.Min(_superThrowParticles, possibleCount);
            StartCoroutine(DoSuperThrow(numThrows));
         }
         else
         {
            ThrowAndRemoveFromInventory();
         }

         _isSuperThrow = false;
      }

      private IEnumerator DoSuperThrow(int numThrows)
      {
         for (int i = 0; i < numThrows; i++)
         {
            ThrowAndRemoveFromInventory();
            yield return new WaitForSeconds(_superThrowDelay);
         }
      }

      private void ThrowAndRemoveFromInventory()
      {
         Sounds.Play("Range");

         var throwableId = _session.QuickInventory.SelectedItem.Id;
         var throwableDef = DefsFacade.I.Throwable.Get(throwableId);
         _throwSpawner.SetPrefab(throwableDef.Projectile);
         _throwSpawner.Spawn();
         
         _session.Data.Inventory.Remove(throwableId, 1);
      }

      public void StartThrowing()
      {
         _superThrowCD.Reset();
      }

      public void StopThrowing()
      {
         if(!_throwCD.IsReady || !CanThrow) return;

         if (_superThrowCD.IsReady) _isSuperThrow = true;
         
         Animator.SetTrigger(ThrowKey);
         
          _throwCD.Reset();
      }

      private void UsePotionAndRemoveFromInventory()
      {
         var UsePotionId = _session.QuickInventory.SelectedItem.Id;
         var UsePotionDef = DefsFacade.I.UsePotion.Get(UsePotionId);
         _health._onChange.Invoke(UsePotionDef.GetHashCode()); // --------------
         _session.Data.Inventory.Remove(UsePotionId, 1);
      }
      
      public void OnUsePotion()
      {
         if (PotionCount <= 0) return;
         _health.ModifyHealth(20);
         _session.Data.Inventory.Remove("Potion(40)", 1);
      }
      
      private bool _isDashing;
      
      public void OnPlayerDash(bool isDashing)
      {
         _isDashing = isDashing;
         
         if (!_dashCD.IsReady) return;
         
         _dashCD.Reset();
         
         switch (transform.localScale.x)
         {
            case -1:
               Rigidbody.AddForce(Vector2.left * _dashForce);
               DashSound();
               break;
            case 1:
               Rigidbody.AddForce(Vector2.right * _dashForce);
               DashSound();
               break;
         }
      }

      private void DashSound()
      {
         Sounds.Play("Dash");
      }

      public void OnOpenPauseMenu()
      {
         _pauseMenu.PauseUnPause();
      }

      public void NextItem()
      {
         _session.QuickInventory.SetNextItem();
      }
   }
}

