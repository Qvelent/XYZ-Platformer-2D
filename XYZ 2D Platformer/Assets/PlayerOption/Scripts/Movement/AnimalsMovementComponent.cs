using UnityEngine;

namespace PlayerOption.Scripts.Movement
{
    public class AnimalsMovementComponent : MonoBehaviour
    {
        [SerializeField] private float _animalsSpeed = 5f;

        private Rigidbody2D _rigidbody2D;
        
       private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
       
       void Update()
       {
           _rigidbody2D.velocity = new Vector2(_animalsSpeed, 0f);
       }
       
       void OnTriggerEnter2D(Collider2D other) 
       {
           _animalsSpeed = -_animalsSpeed;
           FlipEnemyFacing();
       }

       void FlipEnemyFacing() 
       {
           transform.localScale = new Vector2(-(Mathf.Sign(_rigidbody2D.velocity.x)), 1f);
       }
    }
}
