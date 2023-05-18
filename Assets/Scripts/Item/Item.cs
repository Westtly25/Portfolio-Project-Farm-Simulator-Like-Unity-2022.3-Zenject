using UnityEngine;
using Zenject;

public class Item : MonoBehaviour
{
    [ItemCodeDescription]
    [SerializeField]
    private int itemCode;
    public int ItemCode
    {
        get => itemCode;
        set => itemCode = value;
    }

    private SpriteRenderer spriteRenderer;

    [Header("Injected Components")]
    private IInventoryManager inventoryManager;

    [Inject]
    public void Constructor(IInventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if (ItemCode != 0)
        {
            Init(ItemCode);
        }
    }

    public void Init (int itemCodeParam)
    {
        if (itemCodeParam != 0)
        {
            ItemCode = itemCodeParam;

            ItemDetails itemDetails = inventoryManager.GetItemDetails(ItemCode);

            spriteRenderer.sprite = itemDetails.ItemSprite;

            if (itemDetails.ItemType == ItemType.Reapable_scenary)
            {
                gameObject.AddComponent<ItemNudge>();
            }
        }
    }
}

