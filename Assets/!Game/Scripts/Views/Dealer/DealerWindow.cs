using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DealerWindow : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private AutoCollectionData _autoCollectionData;
        [SerializeField] private CellAutoRow _cellAutoRowPrefab;
        [SerializeField] private Transform _cellAutoListContainer;

        private List<CellAutoRow> _cellAutoRows = new();
        
        public static DealerWindow Instance { get; private set; }
        private GameData GameData => GameDataController.Instance.GameData;

        private void Awake()
        {
            _exitButton.onClick.AddListener(OnExitButtonClicked);
            
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            foreach (var autoData in _autoCollectionData.AutoCollection)
            {
                var cellAutoRow = Instantiate(_cellAutoRowPrefab, _cellAutoListContainer);
                cellAutoRow.Setup(autoData.AutoID, autoData.AutoID.ToString());
                _cellAutoRows.Add(cellAutoRow);
            }
            
            gameObject.SetActive(false);
        }

        public float GetPrice(AutoID autoID)
        {
            var cellAutoRow = _cellAutoRows.FirstOrDefault(car => car.AutoID == autoID);
            if (cellAutoRow == null || cellAutoRow.Price == 0f)
            {
                return float.MaxValue;
            }
            
            return cellAutoRow.Price;
        }

        private void OnDestroy()
        {
            _exitButton.onClick.RemoveAllListeners();
        }

        private void OnExitButtonClicked()
        {
            gameObject.SetActive(false);
        }
    }
}
