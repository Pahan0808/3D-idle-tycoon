using UnityEngine;

namespace Game
{
    
    public class GameDataController : MonoBehaviour
    {
        [field: SerializeField] public GameData DefaultGameData { get; private set; }
        [field: SerializeField] public GameData GameData { get; private set; }
        public static GameDataController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            InitializeData();
        }

        private void InitializeData()
        {
            if (GameData == null)
            {
                GameData = ScriptableObject.CreateInstance<GameData>();
            }
        }

        public void ResetToDefault()
        {
            GameData.ResetGameData(DefaultGameData);
        }
    }
}
