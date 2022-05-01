using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerOption.Scripts.Player_Creatures_.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Player_Creatures_.Player.Player _player;
    
        public void OnMovementInput(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _player.SetDirection(direction);
        }

        public void OnInteractInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _player.Interact();
            }
        }
        
        public void OnAttackInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _player.Attack();
            }
        }

        public void OnThrowInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _player.Throw();
            }
            
        }
    }
}
