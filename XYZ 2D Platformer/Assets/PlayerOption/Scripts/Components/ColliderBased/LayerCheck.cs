using UnityEngine;

namespace PlayerOption.Scripts.Components.ColliderBased
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] protected LayerMask _layer;
        [SerializeField] protected bool _isTouchigLayer;
        
        public bool IsTouchingLayer => _isTouchigLayer;
    }
}