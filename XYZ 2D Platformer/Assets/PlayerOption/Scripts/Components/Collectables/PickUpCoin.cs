using PlayerOption.Scripts.Player_Creatures_;
using PlayerOption.Scripts.Player_Creatures_.Player;
using UnityEngine;

namespace PlayerOption.Scripts.Components.Collectables
{
    public class PickUpCoin : MonoBehaviour
    {
        [SerializeField] private int _coins;

        public void PickCoins(GameObject target)
        {
            var allCoins = target.GetComponent<Player>();
            if (allCoins != null)
            {
                allCoins.AddCoins(_coins);
            }
        }
    }
}

