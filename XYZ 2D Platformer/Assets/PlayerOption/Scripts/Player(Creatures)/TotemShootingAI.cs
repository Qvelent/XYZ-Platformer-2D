using PlayerOption.Scripts.Components.ColliderBased;
using PlayerOption.Scripts.Components.GoBased;
using PlayerOption.Scripts.Utils;
using UnityEngine;

namespace PlayerOption.Scripts.Player_Creatures_
{
    public class TotemShootingAI : MonoBehaviour
    {
        [Header("Range")]
        [SerializeField] private CoolDown _rangeCoolDown;
        [SerializeField] private SpawnComponent _rangeAttack;

        [Header("Vision")]
        [SerializeField] private LayerCheck _vision;
        
        private static readonly int Attack = Animator.StringToHash("attack");
        
        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void Update()
        {
            if (!_vision.IsTouchingLayer) return;
            if (_rangeCoolDown.IsReady)
            {
                RangeAttack();
            }
        }

        private void RangeAttack()
        {
            _rangeCoolDown.Reset();
            _animator.SetTrigger(Attack);
        }
        
        public void OnRangeAttackTotemHead()
        {
            _rangeAttack.Spawn();
        }
    }
}