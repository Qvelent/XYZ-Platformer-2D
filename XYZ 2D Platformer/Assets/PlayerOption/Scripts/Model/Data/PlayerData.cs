using System;
using UnityEngine;

namespace PlayerOption.Scripts.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;
        public int Coins;
        public int Hp;
        public bool IsArmed;
        public int Swords;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}
