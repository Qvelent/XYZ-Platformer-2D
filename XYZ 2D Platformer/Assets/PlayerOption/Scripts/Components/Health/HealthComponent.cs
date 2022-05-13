using System;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerOption.Scripts.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHealing;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;

        public void ModifyHealth(int hpDelta)
        {
            if (_maxHealth <= 0) return;
            
            _maxHealth += hpDelta;
            _onChange?.Invoke(_maxHealth);

            if (hpDelta < 0)
            {
                _onDamage?.Invoke();
                Debug.Log("Оставшееся хп: " + _maxHealth);
            }
            
            if (hpDelta > 0)
            {
                _onHealing?.Invoke();
                Debug.Log("Банка похилила: " + _maxHealth);
            }
            
            if (_maxHealth <= 0)
            {
                _onDie?.Invoke();
            }
        }
        
        
#if UNITY_EDITOR
        [ContextMenu("Update Health")]
        private void UpdateHealth()
        {
            _onChange?.Invoke(_maxHealth);
        }

#endif
       
        public void SetHealth(int health)
        {
            _maxHealth = health;
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
