using UnityEngine;

namespace PlayerOption.Scripts.Components
{
    public class TeleporComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destTransform;

        public void Teleport(GameObject target)
        {
            target.transform.position = _destTransform.position;
        }
    }
}

