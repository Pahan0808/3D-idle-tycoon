using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    public class CellAutoRow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _existingAutoCount;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _priceTextbox;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _updatePriceButton;

        private float _price;

        public AutoID AutoID { get; private set; }

        public float Price
        {
            get => _price;
            private set
            {
                _price = value;
                _priceTextbox.text = value.ToString("F2");
            }
        }

        private void Awake()
        {
            _updatePriceButton.onClick.AddListener(OnUpdatePriceClicked);
        }

        private void Update()
        {
            _existingAutoCount.text = $"{GameDataController.Instance.GameData.GetAutoCount(AutoID)} are ready to sell";
        }

        private void OnDestroy()
        {
            _updatePriceButton.onClick.RemoveAllListeners();
        }

        public void Setup(AutoID autoId, string label)
        {
            _label.text = label;
            AutoID = autoId;
        }

        private void OnUpdatePriceClicked()
        {
            Price = float.Parse(_inputField.text);
        }
    }
}
