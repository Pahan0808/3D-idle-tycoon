using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game
{
    public class DealerController : MonoBehaviour
    {
        [SerializeField] private float _timeToChange = 3f;
        [SerializeField] private float _maxPriceChangeMultiplier = 0.1f;
        [SerializeField] private AutoCollectionData _autoCollectionData;

        private float _currentTime;

        public UnityEvent<AutoID, float> PriceChanged;

        public static DealerController Instance { get; private set; }
        private static GameData GameData => GameDataController.Instance.GameData;
        private AutoData[] AutoCollection => _autoCollectionData.AutoCollection;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            foreach (var autoData in AutoCollection)
            {
                var currentPrice = Random.Range(autoData.PriceRange.Min, autoData.PriceRange.Max);
                GameData.SetAutoPrice(autoData.AutoID, currentPrice);
                PriceChanged?.Invoke(autoData.AutoID, currentPrice);
            }
        }

        private void Update()
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime < 0f)
            {
                RefreshPrices();
                _currentTime = _timeToChange;
            }
            
            TryToCell();
        }

        private void OnDestroy()
        {
            PriceChanged.RemoveAllListeners();
        }

        private void ChangePriceAfterCell(AutoData autoData)
        {
            var currentPrice = GameData.GetAutoPrice(autoData.AutoID);
            var changePrice = autoData.PriceRange.Max * _maxPriceChangeMultiplier;
            currentPrice -= changePrice;
            SetPrice(autoData, currentPrice);
        }

        private void RefreshPrices()
        {
            foreach (var autoData in AutoCollection)
            {
                var currentPrice = GameData.GetAutoPrice(autoData.AutoID);
                var maxChangePrice = autoData.PriceRange.Min * _maxPriceChangeMultiplier;
                currentPrice += Random.Range(-maxChangePrice, maxChangePrice);
                SetPrice(autoData, currentPrice);
            }
        }

        private void TryToCell()
        {
            foreach (var autoData in AutoCollection)
            {
                var buyPrice = GameData.GetAutoPrice(autoData.AutoID);
                var cellPrice = DealerWindow.Instance.GetPrice(autoData.AutoID);
                if (buyPrice >= cellPrice && GameData.GetAutoCount(autoData.AutoID) > 0)
                {
                    GameData.IncrementMoney(cellPrice);
                    GameData.DecrementAutos(autoData.AutoID, 1);
                    ChangePriceAfterCell(autoData);
                }
            }
        }

        private void SetPrice(AutoData autoData, float newPrice)
        {
            newPrice = Mathf.Clamp(newPrice, autoData.PriceRange.Min, autoData.PriceRange.Max);
            GameData.SetAutoPrice(autoData.AutoID, newPrice);
            PriceChanged?.Invoke(autoData.AutoID, newPrice);
        }
    }
}
