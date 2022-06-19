using PlayerOption.Scripts.Model;
using PlayerOption.Scripts.Model.Data;
using PlayerOption.Scripts.Utils.Disposables;
using UnityEngine;

namespace PlayerOption.Scripts.UI.Hud.QuickInventary
{
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _prefab;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private GameSession _session;
        private InventoryItemData[] _inventory;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
           
            Rebuild();
        }

        private void Rebuild()
        {
             _inventory = _session.Data.Inventory.GetAll();
            
             // create req items
            for (var i = _createdItem; i < _inventory.Length; i++)
            {
                var item = Instantiate(_prefab, _container);
                _createdItem.Add(item);
            }
            
            // update data and activite
            for (var i = 0; i < _inventory.Length; i++)
            {
                _createdItem[i].SetData(_inventory[i], i);
                _createdItem[i].gameObject.SetActive(true);
            }
            
            // hide unused items
            for (int i = _inventory.Length; i < _createdItem.Count; i++)
            {
                
            }
        }
    }
}