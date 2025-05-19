using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "AutoCollectionData", menuName = "Game/AutoCollectionData")]
    public class AutoCollectionData : ScriptableObject
    {
        [field: SerializeField] public AutoData[] AutoCollection { get; private set; }
    }
}
