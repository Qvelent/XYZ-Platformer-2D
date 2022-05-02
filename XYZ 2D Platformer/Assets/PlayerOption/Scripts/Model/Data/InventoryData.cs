﻿using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PlayerOption.Scripts.Model.Data
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

        public void Add(string id, int value)
        {
            if (value <= 0) return;

            var item = GetItem(id);
            if (item == null)
            {
                item = new InventoryItemData(id);
                _inventory.Add(item);
            }

            item.Value += value;
        }

        public void Remove(string id, int value)
        {
            var item = GetItem(id);
            if (item == null) return;
            {
                item.Value -= value;
            }

            if (item.Value <= 0)
            {
                _inventory.Remove(item);
            }
        }

        private InventoryItemData GetItem(string id)
        {
            foreach (var itemData in _inventory)
            {
                if (itemData.Id == id)
                    return itemData;
            }
            
            return null;
        } 
        
    }
    [Serializable]
    public class InventoryItemData
    {
        public string Id;
        public float Value;

        public InventoryItemData(string id)
        {
            Id = id;
        }
    }
}