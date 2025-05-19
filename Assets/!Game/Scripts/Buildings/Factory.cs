using UnityEngine;

namespace Game
{
    public class Factory : MonoBehaviour, IClickable
    {
        public string Name => "Factory";
        
        public void OnClick()
        {
            FactoryWindow.Instance.gameObject.SetActive(true);
        }
    }
}
