using UnityEngine;

namespace PlayerOption.Scripts.Movement
{
    public class CircularMovementComponent : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private float _speed = 1f;
        private Rigidbody2D[] _bodies;
        private float _time;

        private void Awake()
        {
            UpdateContent();
        }

        private void UpdateContent()
        {
           _bodies = GetComponentsInChildren<Rigidbody2D>();
        }

        private void Update()
        {
            var step = 2 * Mathf.PI / _bodies.Length;

            for (var i = 0; i < _bodies.Length; i++)
            {
                var angle = step * i;
                var pos = new Vector2(
                    Mathf.Cos(angle + _time * _speed) * _radius, 
                    Mathf.Sin(angle + _time * _speed) * _radius);
                
                _bodies[i].MovePosition(pos);
            }

            _time += Time.deltaTime;
        }
    }
}