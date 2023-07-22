using System;

namespace Project.Inventory
{
    public interface IInventoryRead
    {
        InventoryItem GetItem(Guid id);
        InventoryItem[] GetItems();
    }
}