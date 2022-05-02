﻿using System;
using UnityEngine;

namespace PlayerOption.Scripts.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems")]
    public class InventoryItemsDef : ScriptableObject

    {
        [SerializeField] private ItemDef[] _items;

        public ItemDef Get(string id)
        {
            foreach (var itemDef in _items)
            {
                if (itemDef.Id == id)
                    return itemDef;
            }

            return default;
        }
    }
    
    [Serializable]
    public struct ItemDef
    {
        [SerializeField] private string _id; // constructor c# изучить
        public string Id => _id;

        public bool IsVoid => string.IsNullOrEmpty(_id);
    }
}