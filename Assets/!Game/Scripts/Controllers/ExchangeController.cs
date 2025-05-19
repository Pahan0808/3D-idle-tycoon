using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Game
{
    
    public class ExchangeController : MonoBehaviour
    {
        [SerializeField] private float _timeToChange = 3f;
        [SerializeField] private float _maxPriceChangeMultiplier = 0.1f;
        [SerializeField] private MaterialCollectionData _materialCollectionData;
        
        private float _currentTime;
        
        public UnityEvent<ItemID, float> PriceChanged;
        
        public static ExchangeController Instance { get; private set; }
        private static GameData GameData => GameDataController.Instance.GameData;
        private MaterialData[] MaterialCollection => _materialCollectionData.MaterialCollection;

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
            foreach (var material in MaterialCollection)
            {
                var currentPrice = Random.Range(material.PriceRange.Min, material.PriceRange.Max);
                GameData.SetMaterialPrice(material.ItemID, currentPrice);
                PriceChanged?.Invoke(material.ItemID, currentPrice);
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
        }

        private void OnDestroy()
        {
            PriceChanged.RemoveAllListeners();
        }

        private void RefreshPrices()
        {
            foreach (var material in MaterialCollection)
            {
                var currentPrice = GameData.GetMaterialPrice(material.ItemID);
                var maxChangePrice = material.PriceRange.Min * _maxPriceChangeMultiplier;
                currentPrice += Random.Range(-maxChangePrice, maxChangePrice);
                currentPrice = Mathf.Clamp(currentPrice, material.PriceRange.Min, material.PriceRange.Max);
                GameData.SetMaterialPrice(material.ItemID, currentPrice);
                PriceChanged?.Invoke(material.ItemID, currentPrice);
            }
        }
    }
}
