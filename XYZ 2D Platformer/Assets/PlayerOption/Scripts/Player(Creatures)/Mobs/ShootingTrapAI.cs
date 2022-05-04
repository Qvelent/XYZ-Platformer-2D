using System;
using PlayerOption.Scripts.Components.ColliderBased;
using PlayerOption.Scripts.Components.GoBased;
using PlayerOption.Scripts.Utils;
using UnityEngine;

namespace PlayerOption.Scripts.Player_Creatures_.Mobs
{
    public class ShootingTrapAI : MonoBehaviour

    {
        [Header("Melee")]
        [SerializeField] private CoolDown _meleeCoolDown;
        [SerializeField] private CheckCircleOverLap _meleeAttack;
        [SerializeField] private ColliderCheck _meleeCanAttack;
        [Header("Range")]
        [SerializeField] private CoolDown _rangeCoolDown;
        [SerializeField] private SpawnComponent _rangeAttack;
        [Header("Vision")]
        [SerializeField] private ColliderCheck _vision;
        
        private static readonly int Melee = Animator.StringToHash("melee");
        private static readonly int Range = Animator.StringToHash("range");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_vision.IsTouchingLayer)
            {
                if (_meleeCanAttack.IsTouchingLayer)
                {
                    if (_meleeCoolDown.IsReady)
                        MeleeAttack();
                    return;
                }

                if (_rangeCoolDown.IsReady)
                    RangeAttack();
            }
        }

        private void RangeAttack()
        {
            _rangeCoolDown.Reset();
            _animator.SetTrigger(Range);
        }

        private void MeleeAttack()
        {
            _meleeCoolDown.Reset();
            _animator.SetTrigger(Melee);
        }

        public void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }
    }
}