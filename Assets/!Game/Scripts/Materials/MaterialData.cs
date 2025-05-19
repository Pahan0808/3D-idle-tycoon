using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "MaterialData", menuName = "Game/MaterialData")]
    [Serializable]
    public class MaterialData : ItemData
    {
        public Range PriceRange;
    }

    [Serializable]
    public class Range
    {
        public float Min;
        public float Max;
    }
}
    
