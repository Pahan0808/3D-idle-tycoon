using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class FactoryController : MonoBehaviour
    {
        [SerializeField] private AutoCollectionData _autoCollectionData;
        
        public static FactoryController Instance { get; private set; }
        public AutoData[] AutoCollection => _autoCollectionData.AutoCollection;
        private static GameData GameData => GameDataController.Instance.GameData;

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

        public AutoData GetAutoData(AutoID autoID)
        {
            return AutoCollection.FirstOrDefault(a => a.AutoID == autoID);
        }
    }
}
