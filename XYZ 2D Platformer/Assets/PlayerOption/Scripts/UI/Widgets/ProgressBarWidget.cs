﻿using UnityEngine;
using UnityEngine.UI;

namespace PlayerOption.Scripts.UI.Widgets
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