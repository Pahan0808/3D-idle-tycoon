using System.Text;
using TMPro;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MaterialsTextbox : MonoBehaviour
    {
        private TextMeshProUGUI _textbox;

        private void Awake()
        {
            _textbox = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            var text = new StringBuilder();
            foreach (var itemBind in GameDataController.Instance.GameData.Warehouse.Items)
            {
                text.Append($"{itemBind.ItemID} {itemBind.Count}\n");
            }
            _textbox.text = text.ToString();
        }
    }
}
