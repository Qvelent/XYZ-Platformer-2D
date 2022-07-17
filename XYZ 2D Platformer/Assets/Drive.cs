using UnityEngine;

public class Drive : MonoBehaviour
{
    [SerializeField] private float _steerSpeed = 1f;
    [SerializeField] private float _moveSpeed = 0.01f;

  
    void Update()
    {
        float _steerAmount = Input.GetAxis("Horizontal") * _steerSpeed;
        transform.Rotate(0,0, _steerAmount);
        transform.Translate(0, _moveSpeed,0);
    }
}
