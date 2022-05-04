using System;
using UnityEngine;

namespace PlayerOption.Scripts.Components.ColliderBased
{
    public class LineCheck : LayerCheck
    {
        [SerializeField] private Transform _target;

        private readonly RaycastHit2D[] _result = new RaycastHit2D[1];

        private void Update()
        {
            _isTouchigLayer = Physics2D.LinecastNonAlloc(transform.position, _target.position, _result, _layer) > 0;
        }
#if true
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.DrawLine(transform.position, _target.position);
        }
#endif
    }
}