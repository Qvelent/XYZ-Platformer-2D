﻿using UnityEngine;

namespace PlayerOption.Scripts.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/PlayerDef", fileName = "PlayerDef")]
    public class PlayerDef : ScriptableObject
    {
        [SerializeField] private int _inventorySize;
        [SerializeField] private int _maxHealth;

        public int InventorySize => _inventorySize; // C#. Свойства. Аксессоры get, set.
        public int MaxHealth => _maxHealth;
    }
}