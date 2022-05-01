using UnityEngine;

namespace PlayerOption.Scripts.Components.ColliderBased
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private bool _isTouchigLayer;
        private Collider2D _collider;
    
        public bool IsTouchingLayer => _isTouchigLayer;
    
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            _isTouchigLayer = _collider.IsTouchingLayers(_layer);
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            _isTouchigLayer = _collider.IsTouchingLayers(_layer);
        }
    }
}

