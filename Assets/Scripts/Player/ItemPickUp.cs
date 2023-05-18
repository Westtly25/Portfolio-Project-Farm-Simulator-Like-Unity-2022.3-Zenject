using UnityEngine;
using Zenject;

public class ItemPickUp : MonoBehaviour
{
    [Header("Injected Components")]
    private IInventoryManager inventoryManager;

    [Inject]
    public void Constructor(IInventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();

        if (item != null)
        {
            ItemDetails itemDetails = inventoryManager.GetItemDetails(item.ItemCode);

            if (itemDetails.CanBePickedUp == true)
            {
                inventoryManager.AddItem(InventoryLocation.player, item, collision.gameObject);

                AudioManager.Instance.PlaySound(SoundName.effectPickupSound);
            }
        }
    }
}