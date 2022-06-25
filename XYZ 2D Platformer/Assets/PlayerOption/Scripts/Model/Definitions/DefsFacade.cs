using PlayerOption.Scripts.Model.Definitions.Items;
using UnityEngine;

namespace PlayerOption.Scripts.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade",fileName = "DefsFacade")]
    public class DefsFacade : ScriptableObject // singleton c# изучить
    {
        [SerializeField] private InventoryItemsDef _items;
        [SerializeField] private ThrowableItemsDef _throwableItems;
        [SerializeField] private UsePotionItemDef _potions;
        [SerializeField] private PlayerDef _player;

        public InventoryItemsDef Items => _items;

        public ThrowableItemsDef Throwable => _throwableItems;
        public UsePotionItemDef Potions => _potions;
        public PlayerDef Player => _player;

        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade"); // Resources через нее можно указывать путь к папкам в проекте и загружать их
        }
    }
}