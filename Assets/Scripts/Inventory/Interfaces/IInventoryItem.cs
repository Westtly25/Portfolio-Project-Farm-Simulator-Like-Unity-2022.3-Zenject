using System;

namespace Project.Inventory
{
    public interface IInventoryItem
    {
        Guid Id { get; }
        string Title { get; }
        byte StackAmound { get; }
    }
}