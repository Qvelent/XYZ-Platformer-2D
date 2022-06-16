using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.PlayerOption.Scripts.UI.Hud.QuickInventary
{
    public class InventoryItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _selection;
        [SerializeField] private Text _value;

        //private readonly CompositeDisposable _trash = new CompositeDisposable();
    }
}