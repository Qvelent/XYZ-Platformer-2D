using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.PlayerOption.Scripts.UI.Widgets
{
    public class ProgressBarWidget : MonoBehaviour
    {
        [SerializeField] private Image _HpBar;

        public void SetProgress(float progress)
        {
            _HpBar.fillAmount = progress;
        }
    }
}