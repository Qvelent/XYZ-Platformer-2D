using UnityEngine;

namespace PlayerOption.Scripts.Components.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta;

        public void ApplyHealthDelta(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ModifyHealth(_hpDelta);
            }
        }
    }
}

