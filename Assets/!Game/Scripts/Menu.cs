using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _continueGameButton;
        [SerializeField] private Button _exitGameButton;

        private void Awake()
        {
            _newGameButton.onClick.AddListener(OnNewGameClicked);
            _continueGameButton.onClick.AddListener(OnContinueGameClicked);
            _exitGameButton.onClick.AddListener(OnExitGameClicked);
        }

        private void OnDestroy()
        {
            _newGameButton.onClick.RemoveAllListeners();
            _continueGameButton.onClick.RemoveAllListeners();
            _exitGameButton.onClick.RemoveAllListeners();
        }

        private static void OnNewGameClicked()
        {
            SceneManager.LoadScene("Game");
            GameDataController.Instance.ResetToDefault();
        }

        private static void OnContinueGameClicked()
        {
            SceneManager.LoadScene("Game");
            var result = SaveController.Load();
            if (result <= 0)
            {
                GameDataController.Instance.ResetToDefault();
            }
        }

        private static void OnExitGameClicked()
        {
            Application.Quit();
        }
    }
}
