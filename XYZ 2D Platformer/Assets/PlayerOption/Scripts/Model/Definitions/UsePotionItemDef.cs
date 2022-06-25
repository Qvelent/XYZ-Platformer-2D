using System;
using UnityEngine;

namespace PlayerOption.Scripts.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/UsePotionDef", fileName = "UsePotionDef")]
    public class UsePotionItemDef : ScriptableObject
    {
        [SerializeField] private UsePotionDef[] _items;

        public UsePotionDef Get(string id)
        {
            foreach (var itemDef in _items)
            {
                if (itemDef.Id == id)
                    return itemDef;
            }

            return default;
        }
        
        [Serializable] 
        public struct UsePotionDef
        {
            [InventoryId] [SerializeField] private string _id;
            [SerializeField] private GameObject _potions;

            public string Id => _id;

            public GameObject Potions => _potions;
        }
    }
}