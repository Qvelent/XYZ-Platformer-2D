﻿using UnityEngine;

namespace PlayerOption.Scripts.Effects
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private float _effectValue;
        [SerializeField] private Transform _followTatget;

        private float _startX;
        
        private void Start()
        {
            _startX = transform.position.x;
        }

        private void LateUpdate()
        {
            var currentPosition = transform.position;
            var deltaX = _followTatget.position.x * _effectValue;
            transform.position = new Vector3(_startX + deltaX,currentPosition.y,currentPosition.z);
        }
    }
}

