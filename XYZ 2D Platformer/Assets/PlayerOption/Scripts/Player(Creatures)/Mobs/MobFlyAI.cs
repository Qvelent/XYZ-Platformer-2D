using UnityEngine;

namespace PlayerOption.Scripts.Player_Creatures_.Mobs
{
    public class MobFlyAI : MonoBehaviour
    {
        [SerializeField] private Transform[] _poins;
        [SerializeField] private float _moveSpeed;
        
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        
        private int _currentPoint;
        void Start()
        {
            for (int i = 0; i < _poins.Length; i++)
            {
                _poins[i].parent = null;
            }
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position, _poins[_currentPoint].position, _moveSpeed * Time.deltaTime);
            
            if(Vector3.Distance(transform.position, _poins[_currentPoint].position) < .05f)
            {
                _currentPoint++;

                if(_currentPoint >= _poins.Length)
                {
                    _currentPoint = 0;
                }
            }
            
            if(transform.position.x < _poins[_currentPoint].position.x)
            {
                _SpriteRenderer.flipX = true;
            }
            else if(transform.position.x > _poins[_currentPoint].position.x)
            {
                _SpriteRenderer.flipX = false;
            }
        }
    }
}
