using TMPro;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MoneyTextbox : MonoBehaviour
    {
        private TextMeshProUGUI _textbox;

        private void Awake()
        {
            _textbox = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _textbox.text = $"${GameDataController.Instance.GameData.Money:F2}";
        }
    }
}
