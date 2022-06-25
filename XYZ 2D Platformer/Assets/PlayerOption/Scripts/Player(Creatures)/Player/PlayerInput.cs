using PlayerOption.Scripts.Components.Health;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerOption.Scripts.Player_Creatures_.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private ModifyHealthComponent _hpDelta;

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
            if (context.started)
            {
                _player.StartThrowing();
            }
            
            if (context.canceled)
            {
                _player.StopThrowing();
            }
        }

        public void OnUsePotionInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _player.UseInventory();
            }
        }
        
        public void OnUseNextItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _player.NextItem();
            }
        }
    }
}

