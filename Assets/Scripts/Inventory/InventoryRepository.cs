using System;
using System.Collections.Generic;

namespace Project.Inventory
{
    public class InventoryRepository : IInventoryRepositry
    {
        private readonly InventoryContext inventoryContext;

        public InventoryRepository(InventoryContext inventoryContext)
        {
            this.inventoryContext = inventoryContext;
        }

        public IEnumerable<InventoryItem> GetItems()
        {
            throw new NotImplementedException();
        }

        public InventoryItem GetItemByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(InventoryItem item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(InventoryItem item)
        {
            throw new NotImplementedException();
        }
    }
}