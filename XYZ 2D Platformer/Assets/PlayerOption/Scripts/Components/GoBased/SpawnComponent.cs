using UnityEngine;

namespace PlayerOption.Scripts.Components.GoBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private bool _invertXScale;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instantiate = Instantiate(_prefab, _target.position, Quaternion.identity); // Quaternion.identity поворот префаба
            
            var scale = _target.lossyScale;
            scale.x *= _invertXScale ? -1 : 1;
            instantiate.transform.localScale = scale;
            instantiate.SetActive(true);
        }
    }
}
