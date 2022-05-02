using UnityEngine;

namespace PlayerOption.Scripts.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade",fileName = "DefsFacade")]
    public class DefsFacade : ScriptableObject // singleton c# изучить
    {
        [SerializeField] private InventoryItemsDef _items;

        public InventoryItemsDef Items => _items;

        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade"); // Resources через нее можно указывать путь к папкам в проекте и загружать их
        }
    }
}