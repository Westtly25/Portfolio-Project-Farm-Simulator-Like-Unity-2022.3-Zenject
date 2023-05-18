using System;
using System.Collections.Generic;

namespace Project.Inventory
{
    public interface IInventoryRepositry
    {
        IEnumerable<InventoryItem> GetItems();
        InventoryItem GetItemByID(Guid id);
        void Insert(InventoryItem item);
        void Delete(Guid id);
        void Update(InventoryItem item);
    }
}