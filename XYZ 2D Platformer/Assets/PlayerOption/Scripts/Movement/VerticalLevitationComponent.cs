﻿using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayerOption.Scripts.Movement
{
    public class VerticalLevitationComponent : MonoBehaviour
    {
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 1f;
        [SerializeField] private bool _randomize;
        
        private float _originalY;
        private Rigidbody2D _rigidbody2D;
        private float _seed = 1f;
    
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _originalY = _rigidbody2D.transform.position.y;
            
            if (_randomize)
            {
                _seed = Random.value * Mathf.PI * 2; // МАТ ЧАСТЬ !!! 
            }
        }

        private void Update()
        {
            var pos = _rigidbody2D.position;
            pos.y = _originalY + Mathf.Sin(_seed * Time.time * _frequency) * _amplitude; // МАТ ЧАСТЬ !!!
            _rigidbody2D.MovePosition(pos);
        }
    }
}