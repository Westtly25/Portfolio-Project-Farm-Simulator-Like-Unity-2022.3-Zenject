using System;
using Zenject;
using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : IInventoryManager, ISaveable, IInitializable, IDisposable
{
    private Dictionary<int, ItemDetails> itemDetailsDictionary;

    private int[] selectedInventoryItem;

    private List<InventoryItem>[] inventoryList;


    public List<InventoryItem>[] InventoryList
    {
        get => inventoryList;
        private set => inventoryList = value;
    }

    [HideInInspector]
    private int[] inventoryListCapacityIntArray;

    public int[] InventoryListCapacityIntArray
    {
        get => inventoryListCapacityIntArray;
        private set => inventoryListCapacityIntArray = value;
    }

    [SerializeField]
    private ItemListScriptableObject itemList = null;

    private string iSaveableUniqueID;
    public string ISaveableUniqueID
    {
        get => iSaveableUniqueID;
        set => iSaveableUniqueID = value;
    }

    private GameObjectSave gameObjectSave;
    public GameObjectSave GameObjectSave
    {
        get => gameObjectSave;
        set => gameObjectSave = value;
    }

    public void Initialize()
    {
        Setup();
        ISaveableRegister();
    }

    public void Dispose()
    {
        ISaveableDeregister();
    }

    protected void Setup()
    {
        CreateInventoryLists();

        CreateItemDetailsDictionary();

        selectedInventoryItem = new int[(int)InventoryLocation.count];

        for (int i = 0; i < selectedInventoryItem.Length; i++)
        {
            selectedInventoryItem[i] = -1;
        }

        //ISaveableUniqueID = GetComponent<GenerateGUID>().GUID;

        GameObjectSave = new GameObjectSave();

    }

    private void CreateInventoryLists()
    {
        InventoryList = new List<InventoryItem>[(int)InventoryLocation.count];

        for (int i = 0; i < (int)InventoryLocation.count; i++)
        {
            InventoryList[i] = new List<InventoryItem>();
        }

        InventoryListCapacityIntArray = new int[(int)InventoryLocation.count];

        InventoryListCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;
    }

    private void CreateItemDetailsDictionary()
    {
        itemDetailsDictionary = new Dictionary<int, ItemDetails>();

        foreach (ItemDetails itemDetails in itemList.itemDetails)
        {
            itemDetailsDictionary.Add(itemDetails.ItemCode, itemDetails);
        }
    }

    public void AddItem(InventoryLocation inventoryLocation, Item item, GameObject gameObjectToDelete)
    {
        AddItem(inventoryLocation, item);

        //Destroy(gameObjectToDelete);
    }

    public void AddItem(InventoryLocation inventoryLocation, Item item)
    {
        int itemCode = item.ItemCode;
        List<InventoryItem> inventoryList = InventoryList[(int)inventoryLocation];

        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);

        if (itemPosition != -1)
        {
            AddItemAtPosition(inventoryList, itemCode, itemPosition);
        }
        else
        {
            AddItemAtPosition(inventoryList, itemCode);
        }

        EventHandler.CallInventoryUpdatedEvent(inventoryLocation, InventoryList[(int)inventoryLocation]);
    }

    public void AddItem(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = InventoryList[(int)inventoryLocation];

        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);

        if (itemPosition != -1)
        {
            AddItemAtPosition(inventoryList, itemCode, itemPosition);
        }
        else
        {
            AddItemAtPosition(inventoryList, itemCode);
        }

        EventHandler.CallInventoryUpdatedEvent(inventoryLocation, InventoryList[(int)inventoryLocation]);
    }

    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode)
    {
        InventoryItem inventoryItem = new InventoryItem();

        inventoryItem.itemCode = itemCode;
        inventoryItem.itemQuantity = 1;
        inventoryList.Add(inventoryItem);
    }

    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();

        int quantity = inventoryList[position].itemQuantity + 1;
        inventoryItem.itemQuantity = quantity;
        inventoryItem.itemCode = itemCode;
        inventoryList[position] = inventoryItem;
    }

    public void SwapInventoryItems(InventoryLocation inventoryLocation, int fromItem, int toItem)
    {
        if (fromItem < InventoryList[(int)inventoryLocation].Count && toItem < InventoryList[(int)inventoryLocation].Count
             && fromItem != toItem && fromItem >= 0 && toItem >= 0)
        {
            InventoryItem fromInventoryItem = InventoryList[(int)inventoryLocation][fromItem];
            InventoryItem toInventoryItem = InventoryList[(int)inventoryLocation][toItem];

            InventoryList[(int)inventoryLocation][toItem] = fromInventoryItem;
            InventoryList[(int)inventoryLocation][fromItem] = toInventoryItem;

            EventHandler.CallInventoryUpdatedEvent(inventoryLocation, InventoryList[(int)inventoryLocation]);
        }
    }

    public void ClearSelectedInventoryItem(InventoryLocation inventoryLocation)
    {
        selectedInventoryItem[(int)inventoryLocation] = -1;
    }

    public int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = InventoryList[(int)inventoryLocation];

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].itemCode == itemCode)
            {
                return i;
            }
        }

        return -1;
    }

    public ItemDetails GetItemDetails(int itemCode)
    {
        ItemDetails itemDetails;

        if (itemDetailsDictionary.TryGetValue(itemCode, out itemDetails))
        {
            return itemDetails;
        }
        else
        {
            return null;
        }
    }

    public ItemDetails GetSelectedInventoryItemDetails(InventoryLocation inventoryLocation)
    {
        int itemCode = GetSelectedInventoryItem(inventoryLocation);

        if (itemCode == -1)
        {
            return null;
        }
        else
        {
            return GetItemDetails(itemCode);
        }
    }


    private int GetSelectedInventoryItem(InventoryLocation inventoryLocation)
    {
        return selectedInventoryItem[(int)inventoryLocation];
    }

    public string GetItemTypeDescription(ItemType itemType)
    {
        string itemTypeDescription;
        switch (itemType)
        {
            case ItemType.Breaking_tool:
                itemTypeDescription = Settings.BreakingTool;
                break;

            case ItemType.Chopping_tool:
                itemTypeDescription = Settings.ChoppingTool;
                break;

            case ItemType.Hoeing_tool:
                itemTypeDescription = Settings.HoeingTool;
                break;

            case ItemType.Reaping_tool:
                itemTypeDescription = Settings.ReapingTool;
                break;

            case ItemType.Watering_tool:
                itemTypeDescription = Settings.WateringTool;
                break;

            case ItemType.Collecting_tool:
                itemTypeDescription = Settings.CollectingTool;
                break;

            default:
                itemTypeDescription = itemType.ToString();
                break;
        }

        return itemTypeDescription;
    }

    public void ISaveableRegister()
    {
        SaveLoadManager.Instance.iSaveableObjectList.Add(this);
    }

    public void ISaveableDeregister()
    {
        SaveLoadManager.Instance.iSaveableObjectList.Remove(this);
    }

    public GameObjectSave ISaveableSave()
    {
        SceneSave sceneSave = new SceneSave();

        GameObjectSave.sceneData.Remove(Settings.PersistentScene);

        sceneSave.listInvItemArray = InventoryList;

        sceneSave.intArrayDictionary = new Dictionary<string, int[]>();
        sceneSave.intArrayDictionary.Add("inventoryListCapacityArray", inventoryListCapacityIntArray);

        GameObjectSave.sceneData.Add(Settings.PersistentScene, sceneSave);

        return GameObjectSave;
    }


    public void ISaveableLoad(GameSave gameSave)
    {
        if (gameSave.gameObjectData.TryGetValue(ISaveableUniqueID, out GameObjectSave gameObjectSave))
        {
            GameObjectSave = gameObjectSave;

            if (gameObjectSave.sceneData.TryGetValue(Settings.PersistentScene, out SceneSave sceneSave))
            {
                if (sceneSave.listInvItemArray != null)
                {
                    InventoryList = sceneSave.listInvItemArray;

                    for (int i = 0; i < (int)InventoryLocation.count; i++)
                    {
                        EventHandler.CallInventoryUpdatedEvent((InventoryLocation)i, InventoryList[i]);
                    }

                    Player.Instance.ClearCarriedItem();

                    //inventoryBar.ClearHighlightOnInventorySlots();
                }

                if (sceneSave.intArrayDictionary != null && sceneSave.intArrayDictionary.TryGetValue("inventoryListCapacityArray", out int[] inventoryCapacityArray))
                {
                    inventoryListCapacityIntArray = inventoryCapacityArray;
                }
            }

        }
    }

    public void ISaveableStoreScene(string sceneName)
    {
        // Nothing required her since the inventory manager is on a persistent scene;
    }

    public void ISaveableRestoreScene(string sceneName)
    {
        // Nothing required here since the inventory manager is on a persistent scene;
    }


    public void RemoveItem(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = InventoryList[(int)inventoryLocation];

        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);

        if (itemPosition != -1)
        {
            RemoveItemAtPosition(inventoryList, itemCode, itemPosition);
        }

        EventHandler.CallInventoryUpdatedEvent(inventoryLocation, InventoryList[(int)inventoryLocation]);

    }

    private void RemoveItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();

        int quantity = inventoryList[position].itemQuantity - 1;

        if (quantity > 0)
        {
            inventoryItem.itemQuantity = quantity;
            inventoryItem.itemCode = itemCode;
            inventoryList[position] = inventoryItem;
        }
        else
        {
            inventoryList.RemoveAt(position);
        }
    }

    public void SetSelectedInventoryItem(InventoryLocation inventoryLocation, int itemCode)
    {
        selectedInventoryItem[(int)inventoryLocation] = itemCode;
    }
}