using System;
using UnityEngine;

namespace PlayerOption.Scripts.Model.Definitions.Items
{
    [CreateAssetMenu(menuName = "Defs/UsePotionDef", fileName = "UsePotionDef")]
    public class UsePotionItemDef : DefRepository<PotionDef>
    {
    }

    [Serializable]
    public struct PotionDef : IHaveId
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private Effect _effect;
        [SerializeField] private float _value;
        public string Id => _id;

        public Effect Effect => _effect;
        public float Value => _value;

    }

    public enum Effect
    {
        AddHp
    }
}