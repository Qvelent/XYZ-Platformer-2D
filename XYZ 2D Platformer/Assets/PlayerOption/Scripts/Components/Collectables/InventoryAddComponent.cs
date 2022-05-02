using PlayerOption.Scripts.Player_Creatures_.Player;
using UnityEngine;

namespace PlayerOption.Scripts.Components.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private int _count;

        public void Add(GameObject go)
        {
            var player = go.GetComponent<Player>();
            if (player != null)
            {
                player.AddInInventory(_id,_count);
            }
        }
    }
}