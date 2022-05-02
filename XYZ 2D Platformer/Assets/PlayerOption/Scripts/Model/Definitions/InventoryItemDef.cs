using System;
using UnityEngine;

namespace PlayerOption.Scripts.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems")]
    public class InventoryItemDef : ScriptableObject

    {
        [SerializeField] private ItemDef[] _items;
    }

    [Serializable]
    public struct ItemDef
    {
        [SerializeField] private string _id;
        public string Id => _id;
    }
}