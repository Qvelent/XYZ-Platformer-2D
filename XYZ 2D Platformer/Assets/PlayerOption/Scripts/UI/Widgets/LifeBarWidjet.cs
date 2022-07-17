using PlayerOption.Scripts.Components.Health;
using PlayerOption.Scripts.Utils.Disposables;
using UnityEngine;

namespace PlayerOption.Scripts.UI.Widgets
{
    public class LifeBarWidjet : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _hpBar;
        [SerializeField] private HealthComponent _hp;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private int _maxEnemyHP;
        
        
        private void Start()
        {
            if (_hp == null)
            {
                _hp = GetComponentInParent<HealthComponent>();
            }

            _maxEnemyHP = _hp.Health;

            _trash.Retain(_hp._onDie.Subsribe(OnDie));
            _trash.Retain(_hp._onChange.Subsribe(OnHpChanged));
        }

        private void OnDie()
        {
            Destroy(gameObject);
        }

        private void OnHpChanged(int hp)
        {
            var progress = (float) hp / _maxEnemyHP;
            _hpBar.SetProgress(progress);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
            
        }
    }
}