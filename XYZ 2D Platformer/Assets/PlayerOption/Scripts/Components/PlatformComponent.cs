using UnityEngine;

namespace PlayerOption.Scripts.Components
{
    public class PlatformComponent : MonoBehaviour
    {
        [SerializeField] private float _speedPlatform;
        [SerializeField] private Transform[] _points;
        [SerializeField] private Transform _platform;
        [SerializeField] private int _currentTarget;

        private void FixedUpdate()
        {
            _platform.position = Vector3.MoveTowards(
                _platform.position,
                _points[_currentTarget].position,
                _speedPlatform * Time.deltaTime);
            
            if (!(Vector3.Distance(_platform.position, _points[_currentTarget].position) < .05f)) return;
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
    }
}
