using UnityEngine;

namespace PlayerOption.Scripts.Player_Creatures_.Weapons
{
    public class Projectile : BaseProjectile

    {
        protected override void Start()
        {
            base.Start();
            
            var force = new Vector2(Direction * speed, 0);
            Rigidbody.AddForce(force,ForceMode2D.Impulse);
        }
        
        /*
        private void FixedUpdate()
        {
            var position = _rigidbody.position;
            position.x += _direction * _speed;
            _rigidbody.MovePosition(position); //Лучше двигать через рб, чтобы не было проблем с физикой.
        }
        */
    }
}