using PlayerOption.Scripts.Player_Creatures_;
using PlayerOption.Scripts.Player_Creatures_.Player;
using UnityEngine;

namespace PlayerOption.Scripts.Components.Collectables
{
    public class ArmPlayerComponent : MonoBehaviour
    {
        public void ArmPlayer(GameObject go)
        {
            var player = go.GetComponent<Player>();
            if (player != null)
            {
                player.ArmPlayer();
            }
        }
    }
    
}

