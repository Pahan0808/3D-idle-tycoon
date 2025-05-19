using UnityEngine;

namespace Game
{
    public class Dealer : MonoBehaviour, IClickable
    {
        public string Name => "Dealer";
        
        public void OnClick()
        {
            DealerWindow.Instance.gameObject.SetActive(true);
        }
    }
}
