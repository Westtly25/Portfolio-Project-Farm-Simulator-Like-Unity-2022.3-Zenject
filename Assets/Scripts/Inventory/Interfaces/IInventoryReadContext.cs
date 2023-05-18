using System;

namespace Project.Inventory
{
    public interface IInventoryReadContext
    {
        InventoryItem GetItem(Guid id);
        InventoryItem[] GetItems();
    }
}