using PlayerOption.Scripts.Model;
using PlayerOption.Scripts.Model.Data;
using System;
using UnityEngine;


namespace Assets.PlayerOption.Scripts.UI.Hud
{
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _prefab;

        //private readonly CompositeDisposable _trash = new CompositeDisposable();

        private GameSession _session;
        private InventoryItemData[] _inventory;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _inventory = _session.Data.Inventory.GetAll();
            Rebuild();
        }

        private void Rebuild()
        {
            throw new NotImplementedException();
        }
    }
}