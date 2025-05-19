using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Game/ItemData")]
    [Serializable]
    public class ItemData : ScriptableObject
    {
        public ItemID ItemID;
        public string Name;
    }

    public enum ItemID
    {
        Aluminum = 0,
        Plastic = 2,
        Rubber = 3,
        Steel = 1
    }
}
