using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [CreateAssetMenu(fileName = "GameData", menuName = "GameData")]
    [Serializable]
    public class GameData : ScriptableObject
    {
        [SerializeField] private float _money;
        
        [field: SerializeField] public List<AutoPrice> AutoPrices = new();
        [field: SerializeField] public List<MaterialPrice> MaterialPrices = new();
        [field: SerializeField] public Parking Parking;
        [field: SerializeField] public Warehouse Warehouse;

        public float Money
        {
            get => _money;
            private set
            {
                _money = value;
                SaveController.Save();
            }
        }

        public void AddAutos(AutoID autoID, int count)
        {
            Parking.AddAutos(autoID, count);
            SaveController.Save();
        }

        public void AddItems(ItemID itemID, int count)
        {
            Warehouse.AddItems(itemID, count);
            SaveController.Save();
        }
        
        public void DecrementAutos(AutoID autoID, int count)
        {
            Parking.DecrementAutos(autoID, count);
            SaveController.Save();
        }

        public void DecrementItems(IEnumerable<ItemBind> itemBinds)
        {
            Warehouse.DecrementItems(itemBinds);
            SaveController.Save();
        }
        
        public int GetAutoCount(AutoID autoID)
        {
            return Parking.GetAutoCount(autoID);
        }

        public int GetExistingCountOfItemBinds(IEnumerable<ItemBind> itemBinds)
        {
            return Warehouse.GetExistingCountOfItemBinds(itemBinds);
        }

        public float GetAutoPrice(AutoID id)
        {
            var autoPrice = AutoPrices.FirstOrDefault(x => x.AutoID == id);
            return autoPrice?.Price ?? 0f;
        }

        public float GetMaterialPrice(ItemID id)
        {
            var materialPrice = MaterialPrices.FirstOrDefault(x => x.ItemID == id);
            return materialPrice?.Price ?? 0f;
        }

        public void IncrementMoney(float value)
        {
            Money += value;
        }
        
        public void SetAutoPrice(AutoID id, float price)
        {
            var autoPrice = AutoPrices.FirstOrDefault(x => x.AutoID == id);
            if (autoPrice != null)
            {
                autoPrice.Price = price;
            }
            SaveController.Save();
        }
        
        public void SetMaterialPrice(ItemID id, float price)
        {
            var materialPrice = MaterialPrices.FirstOrDefault(x => x.ItemID == id);
            if (materialPrice != null)
            {
                materialPrice.Price = price;
            }
            SaveController.Save();
        }

        public void ResetGameData(GameData defaultGameData)
        {
            AutoPrices = new List<AutoPrice>();
            foreach (var autoPrice in defaultGameData.AutoPrices)
            {
                var newAutoPrice = new AutoPrice() { AutoID = autoPrice.AutoID, Price = autoPrice.Price };
                AutoPrices.Add(newAutoPrice);
            }
            
            Money = defaultGameData.Money;
            
            MaterialPrices = new List<MaterialPrice>();
            foreach (var materialPrice in defaultGameData.MaterialPrices)
            {
                var newMaterialPrice = new MaterialPrice() { ItemID = materialPrice.ItemID, Price = materialPrice.Price };
                MaterialPrices.Add(newMaterialPrice);
            }
            
            Parking.Autos = new List<AutoBind>();
            foreach (var autoBind in defaultGameData.Parking.Autos)
            {
                var newAutoBind = new AutoBind() { AutoID = autoBind.AutoID, Count = autoBind.Count };
                Parking.Autos.Add(newAutoBind);
            }

            Warehouse.Items = new List<ItemBind>();
            foreach (var itemBind in defaultGameData.Warehouse.Items)
            {
                var newItemBind = new ItemBind() { ItemID = itemBind.ItemID, Count = itemBind.Count };
                Warehouse.Items.Add(newItemBind);
            }
        }
    }
    
    

    [Serializable]
    public class AutoPrice
    {
        public AutoID AutoID;
        public float Price;
    }

    [Serializable]
    public class MaterialPrice
    {
        public ItemID ItemID;
        public float Price;
    }
}