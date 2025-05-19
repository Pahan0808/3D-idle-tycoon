using TMPro;
using UnityEngine;

namespace Game
{
    public class MaterialBind : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameTextbox;
        [SerializeField] private TMP_Text _countTextbox;
        
        public string Name { set => _nameTextbox.text = value; }
        public string Count { set => _countTextbox.text = value; }
    }
}
