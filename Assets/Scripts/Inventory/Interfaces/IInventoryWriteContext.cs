using System;

namespace Project.Inventory
{
    public interface IInventoryWriteContext
    {
        bool Add(InventoryItem inventoryItem);
        bool UpdateQuantity(Guid id, int quantity);
        bool Swape(Guid firstItemId, Guid secondItemId);
    }
}