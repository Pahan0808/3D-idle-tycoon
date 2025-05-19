using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "MaterialCollectionData", menuName = "Game/MaterialCollectionData")]
    [Serializable]
    public class MaterialCollectionData : ScriptableObject
    {
        [field: SerializeField] public MaterialData[] MaterialCollection { get; private set; }
    }
}
