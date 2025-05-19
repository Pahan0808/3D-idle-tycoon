using UnityEngine;

namespace Game
{
    public class Exchange : MonoBehaviour, IClickable
    {
        public string Name => "Exchange";
        
        public void OnClick()
        {
            ExchangeWindow.Instance.gameObject.SetActive(true);
        }
    }
}
