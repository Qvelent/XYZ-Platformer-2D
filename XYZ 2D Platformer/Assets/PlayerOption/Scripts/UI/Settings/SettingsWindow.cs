﻿using Assets.PlayerOption.Scripts.Model.Data;
using PlayerOption.Scripts.UI.Widgets;
using UnityEngine;

namespace PlayerOption.Scripts.UI.Settings
{
    public class SettingsWindow : AnimatedWindow
    {
        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;

        protected override void Start()
        {
            base.Start();

            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);
        }
    }
}