using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace PlayerOption.Scripts.Camera
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _damping;

        private void LateUpdate()
        {
            var destination = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * _damping); // Функция интерполяции  Vector3.Lerp сглаживает от точки A до B.
        }
    }
}
