using UnityEngine;
using UnityEngine.UI;

namespace PlayerOption.Scripts.Components
{
    public class PlatformComponent : MonoBehaviour
    {
        [SerializeField] private bool _invertScale;
        [SerializeField] private float _speedPlatform;
        [SerializeField] private Transform[] _points;
        [SerializeField] private Transform _platform;
    
        private int _currentTarget;
        private Vector2 Direction;
        
        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        private void FixedUpdate()
        {
            var xVelocity = Direction.x * _speedPlatform;

            _platform.position = Vector3.MoveTowards(
                _platform.position,
                _points[_currentTarget].position,
                _speedPlatform * Time.deltaTime);
            
            if (!(Vector3.Distance(_platform.position, _points[_currentTarget].position) < .05f)) return;
            Flip(Direction);
            _currentTarget++;

            if (_currentTarget >= _points.Length)
            {
                _currentTarget = 0;
            }

            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.parent = transform;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.parent = null;
            }
        }

        private void Flip(Vector2 direction)
        {
            var multiplier = _invertScale ? -1 : 1;
            
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier,1,1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3( -1 * multiplier, 1, 1);
            }
        }
    }
}
