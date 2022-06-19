using System;
using Assets.PlayerOption.Scripts.Model.Data.Properties;
using PlayerOption.Scripts.Model.Definitions;
using PlayerOption.Scripts.Utils.Disposables;
using UnityEngine;

namespace PlayerOption.Scripts.Model.Data
{
    public class QuickInventoryModel
    {
        private readonly PlayerData _data;
        
        public InventoryItemData[] Inventory { get; private set; }

        public readonly IntProperty SelectedIndex = new IntProperty();

        public event Action OnChanged;

        public QuickInventoryModel(PlayerData data)
        {
            _data = data;

            Inventory = _data.Inventory.GetAll(ItemTag.Usable);
            _data.Inventory.OnChanged += OnChangedInventory;
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        private void OnChangedInventory(string id, int value)
        {
            var indexFound = Array.FindIndex(Inventory, x => x.Id == id);
            if (indexFound == -1) return;
            Inventory = _data.Inventory.GetAll(ItemTag.Usable);
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
            OnChanged?.Invoke();
        }
    }
}