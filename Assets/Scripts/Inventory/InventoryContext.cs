using System;
using UnityEngine;

namespace Project.Inventory
{
    [Serializable]
    public class InventoryContext : IInventoryContext
    {
        [SerializeField] private readonly InventoryItem[] inventoryItems;

        public InventoryContext(int size)
        {
            inventoryItems = new InventoryItem[size];
        }

        public InventoryItem GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public InventoryItem[] GetItems() => inventoryItems;

        public bool Add(InventoryItem inventoryItem)
        {
            inventoryItems[0] = inventoryItem;

            throw new NotImplementedException();
        }

        public bool UpdateQuantity(Guid id, int quantity)
        {
            throw new NotImplementedException();
        }

        public bool Swape(Guid firstItemId, Guid secondItemId)
        {
            throw new NotImplementedException();
        }

        private bool Stack()
        {
            throw new NotImplementedException();
        }
    }
}