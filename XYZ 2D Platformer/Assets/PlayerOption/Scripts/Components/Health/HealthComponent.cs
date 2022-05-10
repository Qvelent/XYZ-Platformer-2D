using System;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerOption.Scripts.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHealing;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;

        public float invincibleLength;
        private float _invincibleCounter;

        private SpriteRenderer _playerSr;

        private void Start()
        {
            _playerSr = GetComponent<SpriteRenderer>();
        }

    
        private void Update()
        {
            if (!(_invincibleCounter > 0)) return;
            _invincibleCounter -= Time.deltaTime;

            if (!(_invincibleCounter <= 0)) return;
            var color = _playerSr.color;
            color = new Color(color.r, color.g, color.b, 1f);
            _playerSr.color = color;
        }
        
        public void ModifyHealth(int healthDelta)
        {
            if (_health <= 0) return;
            
            _health += healthDelta; //  x = x + y
            _onChange?.Invoke(_health);
            
            if (healthDelta > 0)
            {
                _onHealing?.Invoke();
                Debug.Log("Банка похилила: " + _health);
            }

            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }
        
        public void DealDamage(int hpDelta)
        {
            if (!(_invincibleCounter <= 0)) return;

            if (hpDelta >= 0 && _invincibleCounter != 0) return; // Подумать почему прилетает дамаг
            _onDamage?.Invoke(); // if(_onDamage != null) { _onDamage.Invoke(); }
            _invincibleCounter = invincibleLength;
            var color = _playerSr.color;
            color = new Color(color.r, color.g, color.b, .5f);
            _playerSr.color = color;
        }
        
        
#if UNITY_EDITOR
        [ContextMenu("Update Health")]
        private void UpdateHealth()
        {
            _onChange?.Invoke(_health);
        }

#endif
       
        public void SetHealth(int health)
        {
            _health = health;
        }

        private void OnDestroy()
        {
            _onDie.RemoveAllListeners();
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {
            
        }
        
    }
}
