using Assets.PlayerOption.Scripts.UI.Widgets;
using PlayerOption.Scripts.Model;
using PlayerOption.Scripts.Model.Definitions;
using PlayerOption.Scripts.Utils;
using UnityEngine;

namespace PlayerOption.Scripts.UI.Hud
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;

        private GameSession _session;
        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _session.Data.Hp.OnChanged += OnHealthChanged;

            OnHealthChanged(_session.Data.Hp.Value, 0   );
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = DefsFacade.I.Player.MaxHealth;
            var value = (float)newValue / maxHealth;
            _healthBar.SetProgress(value);
        }

        public void OnSetting()
        {
            WindowUntils.CreateWindow("UI/PauseMenuWindow");
        }

        private void OnDestroy()
        {
            _session.Data.Hp.OnChanged -= OnHealthChanged;
        }
    }
}