using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ExchangeWindow : MonoBehaviour
    {
        private const float MaxSliderValue = 1000f;
        
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private MaterialCollectionData _materialCollectionData;
        [SerializeField] private MaterialRow _materialRowPrefab;
        [SerializeField] private Transform _materialsListContainer;
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _sliderValueTextbox;
        [SerializeField] private ToggleGroup _toggleGroup;

        private int _countToBuy;
        private List<MaterialRow> _materialRows = new();
        private ItemID _selectedItemID;
        
        public static ExchangeWindow Instance { get; private set; }
        private GameData GameData => GameDataController.Instance.GameData;

        private void Awake()
        {
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
            
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            foreach (var materialData in _materialCollectionData.MaterialCollection)
            {
                var materialRow = Instantiate(_materialRowPrefab, _materialsListContainer);
                materialRow.Setup(materialData.ItemID, materialData.Name, _toggleGroup);
                materialRow.ToggleEnabled.AddListener(OnToggleEnabled);
                _materialRows.Add(materialRow);
                OnToggleEnabled(materialData.ItemID);
            }
            
            ExchangeController.Instance.PriceChanged.AddListener(OnPriceChanged);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _slider.onValueChanged.RemoveAllListeners();
        }

        private void OnBuyButtonClicked()
        {
            GameData.IncrementMoney(-GameData.GetMaterialPrice(_selectedItemID) * _countToBuy);
            GameData.AddItems(_selectedItemID, _countToBuy);
            
            var materialRow = _materialRows.FirstOrDefault(mr => mr.ItemID == _selectedItemID);
            if (materialRow == null) return;
            _slider.maxValue = GameData.Money / materialRow.Price;
            _slider.value = Mathf.Min(_slider.value, _slider.maxValue);
        }

        private void OnExitButtonClicked()
        {
            gameObject.SetActive(false);
        }

        private void OnPriceChanged(ItemID itemID, float price)
        {
            var materialRow = _materialRows.FirstOrDefault(mr => mr.ItemID == itemID);
            if (materialRow == null) return;
            
            materialRow.Price = price;
            if (_selectedItemID == itemID)
            {
                RefreshSlider(GameData.Money / materialRow.Price);
            }
        }

        private void OnSliderValueChanged(float newValue)
        {
            _countToBuy = Mathf.RoundToInt(newValue);
            _sliderValueTextbox.SetText(_countToBuy.ToString());
        }

        private void OnToggleEnabled(ItemID itemID)
        {
            _selectedItemID = itemID;
            var materialRow = _materialRows.FirstOrDefault(mr => mr.ItemID == itemID);
            if (materialRow == null)
            {
                RefreshSlider(0f);
                return;
            }

            RefreshSlider(GameData.Money / materialRow.Price);
        }

        private void RefreshSlider(float maxValue)
        {
            _slider.maxValue = Mathf.Min(maxValue, MaxSliderValue);
            _slider.value = Mathf.Min(_slider.value, _slider.maxValue);
        }
    }
}
