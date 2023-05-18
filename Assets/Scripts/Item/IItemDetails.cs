using UnityEngine;

public interface IItemDetails
{
    public int ItemCode { get; set; }
    public ItemType ItemType { get; set; }
    public string ItemDescription { get; set; }
    public Sprite ItemSprite { get; set; }
    public string ItemLongDescription { get; set; }
    public short ItemUseGridRadius { get; set; }
    public float ItemUseRadius { get; set; }
    public bool IsStartingItem { get; set; }
    public bool CanBePickedUp { get; set; }
    public bool CanBeDropped { get; set; }
    public bool CanBeEaten { get; set; }
    public bool CanBeCarried { get; set; }
}
