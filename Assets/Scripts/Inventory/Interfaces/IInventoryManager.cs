using System.Collections.Generic;
using UnityEngine;

public interface IInventoryManager
{
    List<InventoryItem>[] InventoryList { get; }
    int[] InventoryListCapacityIntArray { get; }

    void AddItem(InventoryLocation inventoryLocation, Item item);
    void AddItem(InventoryLocation inventoryLocation, Item item, GameObject gameObjectToDelete);
    void RemoveItem(InventoryLocation inventoryLocation, int itemCode);
    void SwapInventoryItems(InventoryLocation inventoryLocation, int fromItem, int toItem);
    void ClearSelectedInventoryItem(InventoryLocation inventoryLocation);
    void SetSelectedInventoryItem(InventoryLocation inventoryLocation, int itemCode);
    ItemDetails GetItemDetails(int itemCode);
    ItemDetails GetSelectedInventoryItemDetails(InventoryLocation inventoryLocation);
    string GetItemTypeDescription(ItemType itemType);
    int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode);
}
