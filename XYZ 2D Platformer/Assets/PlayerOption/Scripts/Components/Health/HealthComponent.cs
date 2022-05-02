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
        
        public void ModifyHealth(int healthDelta)
        {
            if (_health <= 0) return;
            
            _health += healthDelta; //  x = x + y
            _onChange?.Invoke(_health);
            
            if (healthDelta < 0) // Если хп героя меньше 0 
            {
                _onDamage?.Invoke(); // if(_onDamage != null) { _onDamage.Invoke(); }
                Debug.Log("Health: " + _health);
            }

            if (healthDelta > 0)
            {
                _onHealing?.Invoke();
                Debug.Log("Health: " + _health);
            }

            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
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
        
        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {
            
        }
        
    }
}
