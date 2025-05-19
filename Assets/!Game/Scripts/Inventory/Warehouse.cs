using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Warehouse
    {
        [field: SerializeField] public List<ItemBind> Items { get; set; } = new();

        public void AddItems(ItemID itemID, int count)
        {
            var itemBind = Items.FirstOrDefault(ib => ib.ItemID == itemID);
            if (itemBind == null)
            {
                itemBind = new ItemBind() { ItemID = itemID, Count = count };
                Items.Add(itemBind);
            }
            else
            {
                itemBind.Count += count;
            }
        }

        public void DecrementItems(IEnumerable<ItemBind> items)
        {
            foreach (var itemBind in CombineItems(items))
            {
                var existingItems = GetItemBind(itemBind.ItemID);
                if (existingItems == null)
                {
                    continue;
                }
                existingItems.Count -= itemBind.Count;
                existingItems.Count = Mathf.Max(0, existingItems.Count);
            }
        }

        public int GetExistingCountOfItemBinds(IEnumerable<ItemBind> items)
        {
            var count = 1;
            var combinedItems = CombineItems(items);
            if(combinedItems.Sum(ci => ci.Count) <= 0) return int.MaxValue;
            
            while (true)
            {
                foreach (var combinedItemBind in combinedItems)
                {
                    var existingItemBind = GetItemBind(combinedItemBind.ItemID);
                    if (existingItemBind == null || existingItemBind.Count < combinedItemBind.Count * count)
                    {
                        return --count;
                    }
                }
                count++;
            }
        }

        public bool IsEnoughItems(IEnumerable<ItemBind> items)
        {
            foreach (var itemBind in CombineItems(items))
            {
                var existingItems = GetItemBind(itemBind.ItemID);
                if(existingItems == null || existingItems.Count < itemBind.Count) return false;
            }
            return true;
        }

        private IEnumerable<ItemBind> CombineItems(IEnumerable<ItemBind> items)
        {
            return items.GroupBy(i => i.ItemID)
                .Select(g => new ItemBind
                {
                    ItemID = g.Key,
                    Count = g.Sum(i => i.Count)
                }).ToList();
        }

        private ItemBind GetItemBind(ItemID itemID)
        {
            return Items.FirstOrDefault(i => i.ItemID == itemID);
        }
    }

    [Serializable]
    public class ItemBind
    {
        public ItemID ItemID;
        public int Count;
    }
}
