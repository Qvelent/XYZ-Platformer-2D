using PlayerOption.Scripts.Player_Creatures_;
using PlayerOption.Scripts.Player_Creatures_.Player;
using UnityEngine;

namespace PlayerOption.Scripts.Components
{
    public class PickSwordComponent : MonoBehaviour
    {
        [SerializeField] private int _swords;

        public void PickSwords(GameObject target)
        {
            var allSowrds = target.GetComponent<Player>();
            if (allSowrds != null)
            {
                allSowrds.AddSwords(_swords);
            }
        }
    }
}