using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace PlayerOption.Scripts.UI.Widgets
{
    public class CustomButtom : Button
    {
        [SerializeField] private GameObject _normal;
        [SerializeField] private GameObject _pressed;

        protected override void DoStateTransition(SelectionState state,bool instant)
        {
            base.DoStateTransition(state, instant);
            
            _normal.SetActive(state != SelectionState.Pressed);
            _pressed.SetActive(state == SelectionState.Pressed);
            
        }
    }
}