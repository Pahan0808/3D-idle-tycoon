using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "AutoData", menuName = "Game/AutoData")]
    public class AutoData : ScriptableObject
    {
        [field: SerializeField] public AutoID AutoID { get; private set; }
        [field: SerializeField] public ItemBind[] ItemsToBuild { get; private set; }
        [field: SerializeField] public Range PriceRange { get; private set; }
    }

    [Serializable]
    public enum AutoID
    {
        CarA = 0,
        CarB = 1,
        CarC = 2,
        CarD = 3
    }
}
