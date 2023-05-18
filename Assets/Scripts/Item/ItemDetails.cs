using UnityEngine;

[System.Serializable]
public class ItemDetails : IItemDetails
{
    [SerializeField]
    private int itemCode;
    [SerializeField]
    private ItemType itemType;
    [SerializeField]
    private string itemDescription;
    [SerializeField]
    private Sprite itemSprite;
    [SerializeField]
    private string itemLongDescription;
    [SerializeField]
    private short itemUseGridRadius;
    [SerializeField]
    private float itemUseRadius;
    [SerializeField]
    private bool isStartingItem;
    [SerializeField]
    private bool canBePickedUp;
    [SerializeField]
    private bool canBeDropped;
    [SerializeField]
    private bool canBeEaten;
    [SerializeField]
    private bool canBeCarried;

    public int ItemCode
    {
        get => itemCode;
        set => itemCode = value;
    }
    public ItemType ItemType
    {
        get => itemType;
        set => itemType = value;
    }
    public string ItemDescription
    {
        get => itemDescription;
        set => itemDescription = value;
    }
    public Sprite ItemSprite
    {
        get => itemSprite;
        set => itemSprite = value;
    }
    public string ItemLongDescription
    {
        get => itemLongDescription;
        set => itemLongDescription = value;
    }
    public short ItemUseGridRadius
    {
        get => itemUseGridRadius;
        set => itemUseGridRadius = value;
    }
    public float ItemUseRadius
    {
        get => itemUseRadius;
        set => itemUseRadius = value;
    }
    public bool IsStartingItem
    {
        get => isStartingItem;
        set => isStartingItem = value;
    }
    public bool CanBePickedUp
    {
        get => canBePickedUp;
        set => canBePickedUp = value;
    }
    public bool CanBeDropped
    {
        get => canBeDropped;
        set => canBeDropped = value;
    }
    public bool CanBeEaten
    {
        get => canBeEaten;
        set => canBeEaten = value;
    }
    public bool CanBeCarried
    {
        get => canBeCarried;
        set => canBeCarried = value;
    }
}