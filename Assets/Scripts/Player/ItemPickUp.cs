using Zenject;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [Header("Injected Components")]
    private IInventoryManager inventoryManager;
    private AudioService audioService;

    [Inject]
    public void Constructor(IInventoryManager inventoryManager, AudioService audioService)
    {
        this.inventoryManager = inventoryManager;
        this.audioService = audioService;
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

                audioService.PlaySound(SoundName.effectPickupSound);
            }
        }
    }
}