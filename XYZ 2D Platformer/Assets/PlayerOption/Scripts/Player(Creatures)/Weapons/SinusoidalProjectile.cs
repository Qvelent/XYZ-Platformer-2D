﻿using System;
using UnityEngine;

namespace PlayerOption.Scripts.Player_Creatures_.Weapons
{
    public class SinusoidalProjectile : BaseProjectile
    {
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 1f;
        private float _originalY;
        private float _time;
        
        protected override void Start()
        {
            base.Start();
            _originalY = Rigidbody.position.y;
        }

        private void Update()
        {
            var position = Rigidbody.position;
            position.x += Direction * speed;
            position.y =  _originalY + Mathf.Sin(_time * _frequency) * _amplitude;
            Rigidbody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }
    }
}