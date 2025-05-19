using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class MaterialRow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _priceTextbox;
        [SerializeField] private Toggle _toggle;

        private float _price;

        public UnityEvent<ItemID> ToggleEnabled;

        public ItemID ItemID { get; private set; }

        public float Price
        {
            get => _price;
            set
            {
                _price = value;
                _priceTextbox.text = value.ToString("F2");
            }
        }

        private void Awake()
        {
            _toggle.onValueChanged.AddListener(OnToggleChanged);
        }

        public void Setup(ItemID itemID, string label, ToggleGroup toggleGroup)
        {
            _label.text = label;
            _toggle.group = toggleGroup;
            ItemID = itemID;
            Price = GameDataController.Instance.GameData.GetMaterialPrice(ItemID);
        }

        private void OnToggleChanged(bool isOn)
        {
            if (isOn)
            {
                ToggleEnabled?.Invoke(ItemID);
            }
        }
    }
}