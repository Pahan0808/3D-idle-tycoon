using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class FactoryWindow : MonoBehaviour
    {
        private const float MaxSliderValue = 100f;
        
        [SerializeField] private AutoRow _autoRowPrefab;
        [SerializeField] private Button _createButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Transform _autoListContainer;
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _sliderValueTextbox;
        [SerializeField] private ToggleGroup _toggleGroup;

        private int _countToBuy;
        private List<AutoRow> _autoRows = new();
        private AutoID _selectedAutoID;

        public static FactoryWindow Instance { get; private set; }
        private static GameData GameData => GameDataController.Instance.GameData;

        private void Awake()
        {
            _createButton.onClick.AddListener(OnCreateButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
            _slider.onValueChanged.AddListener(OnSliderValueChanged);

            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void OnEnable()
        {
            OnToggleEnabled(_selectedAutoID);
        }

        private void Start()
        {
            foreach (var autoData in FactoryController.Instance.AutoCollection)
            {
                var autoRow = Instantiate(_autoRowPrefab, _autoListContainer);
                autoRow.Setup(autoData.AutoID, autoData.AutoID.ToString(), _toggleGroup);
                autoRow.ToggleEnabled.AddListener(OnToggleEnabled);
                _autoRows.Add(autoRow);
                OnToggleEnabled(autoData.AutoID);
            }

            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _createButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _slider.onValueChanged.RemoveAllListeners();
        }
        
        #region Callbacks

        private void OnCreateButtonClicked()
        {
            var autoPrice = GetAutoPrice();
            if (autoPrice == null) return;
            
            GameData.DecrementItems(GetSumAutoPrice(autoPrice));
            GameData.AddAutos(_selectedAutoID, _countToBuy);
            
            RefreshSlider(GameData.GetExistingCountOfItemBinds(autoPrice));
        }

        private void OnExitButtonClicked()
        {
            gameObject.SetActive(false);
        }

        private void OnSliderValueChanged(float newValue)
        {
            _countToBuy = Mathf.RoundToInt(newValue);
            _sliderValueTextbox.SetText(_countToBuy.ToString());
        }

        private void OnToggleEnabled(AutoID autoID)
        {
            _selectedAutoID = autoID;
            
            var autoPrice = GetAutoPrice();
            if (autoPrice == null)
            {
                RefreshSlider(0f);
                return;
            }

            RefreshSlider(GameData.GetExistingCountOfItemBinds(autoPrice));
        }

        #endregion

        private IEnumerable<ItemBind> GetAutoPrice()
        {
            var autoData = FactoryController.Instance.AutoCollection.FirstOrDefault(auto => auto.AutoID == _selectedAutoID);
            if (autoData == null) return null;

            return autoData.ItemsToBuild
                .Select(ib => new ItemBind
                {
                    ItemID = ib.ItemID,
                    Count = ib.Count
                }).ToList();
        }

        private IEnumerable<ItemBind> GetSumAutoPrice(IEnumerable<ItemBind> itemBinds)
        {
            return itemBinds.Select(ib => new ItemBind
            {
                ItemID = ib.ItemID,
                Count = ib.Count * _countToBuy
            }).ToList();
        }

        private void RefreshSlider(float maxValue)
        {
            Debug.Log($"maxValue: {maxValue}  refresh");
            _slider.maxValue = Mathf.Min(maxValue, MaxSliderValue);
            _slider.value = Mathf.Min(_slider.value, _slider.maxValue);
        }
    }
}
