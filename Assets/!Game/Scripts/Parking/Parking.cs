using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Parking
    {
        [field: SerializeField] public List<AutoBind> Autos { get; set; } = new();

        public void AddAutos(AutoID autoID, int count)
        {
            var autoBind = Autos.FirstOrDefault(ab => ab.AutoID == autoID);
            if (autoBind == null)
            {
                autoBind = new AutoBind() { AutoID = autoID, Count = count };
                Autos.Add(autoBind);
            }
            else
            {
                autoBind.Count += count;
            }
        }
        
        public void DecrementAutos(AutoID autoID, int count)
        {
            var autoBind = Autos.FirstOrDefault(ab => ab.AutoID == autoID);
            if (autoBind == null)
            {
                return;
            }

            autoBind.Count -= count;
        }
        
        public int GetAutoCount(AutoID autoID)
        {
            var autoBind = Autos.FirstOrDefault(ab => ab.AutoID == autoID);
            return autoBind?.Count ?? 0;
        }
    }

    [Serializable]
    public class AutoBind
    {
        public AutoID AutoID;
        public int Count;
    }
}