using System.Collections;
using PlayerOption.Scripts.Components.ColliderBased;
using PlayerOption.Scripts.Components.GoBased;
using PlayerOption.Scripts.Player_Creatures_.Mobs.Patrolling;
using UnityEngine;

namespace PlayerOption.Scripts.Player_Creatures_.Mobs
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private ColliderCheck _vision;
        [SerializeField] private ColliderCheck _canAttack;

        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _missPlayerCooldown = 0.5f;

        private IEnumerator _current;
        private GameObject _target;

        private SpawnListComponent _particles;
        private Creature _creature;
        private Animator _animator;
        private bool _isDead;
        private Patrol _patrol;


        private static readonly int IsDieKey = Animator.StringToHash("is-dead");

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }
        
        public void OnPlayerVision(GameObject go)
        {
            if(_isDead) return;
            
            _target = go;
            StartCoroutine(AgroToPlayer());
        }

        private IEnumerator AgroToPlayer()
        {
            LookAtPlayer();
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);
            
            StartState(GoToPlayer());
        }

        private void LookAtPlayer()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(Vector2.zero);
            _creature.Flip(direction);
        }

        private IEnumerator GoToPlayer()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }
                yield return null;
            }

            _creature.SetDirection(Vector2.zero);
            _particles.Spawn("Miss");
            yield return new WaitForSeconds(_missPlayerCooldown);

            StartState(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }
            
            StartState(GoToPlayer());
        }
        
        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(direction);
        }

        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector3.zero);
            
            if (_current != null)
            {
                StopCoroutine(_current);
            }

            _current = coroutine;
            StartCoroutine(coroutine);
        }
        
        public void OnDie()
        {
            
            _isDead = true;
            _animator.SetBool(IsDieKey, true);
            
            _creature.SetDirection(Vector2.zero);
            if (_current != null)
            {
                StopCoroutine(_current);
            }
        }
    }
}