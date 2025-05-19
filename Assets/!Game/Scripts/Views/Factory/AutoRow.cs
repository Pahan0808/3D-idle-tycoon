using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class AutoRow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private MaterialBind _materialBindPrefab;
        [SerializeField] private Transform _materialCostRow;
        [SerializeField] private Toggle _toggle;

        private float _price;

        public UnityEvent<AutoID> ToggleEnabled;

        public AutoID AutoID { get; private set; }

        private void Awake()
        {
            _toggle.onValueChanged.AddListener(OnToggleChanged);
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveAllListeners();
        }

        public void Setup(AutoID autoID, string label, ToggleGroup toggleGroup)
        {
            _label.text = label;
            _toggle.group = toggleGroup;
            AutoID = autoID;
            var autoData = FactoryController.Instance.GetAutoData(autoID);
            foreach (var itemBind in autoData.ItemsToBuild)
            {
                var materialBind = Instantiate(_materialBindPrefab, _materialCostRow);
                materialBind.Name = itemBind.ItemID.ToString();
                materialBind.Count = itemBind.Count.ToString();
            }
        }

        private void OnToggleChanged(bool isOn)
        {
            if (isOn)
            {
                ToggleEnabled?.Invoke(AutoID);
            }
        }
    }
}
