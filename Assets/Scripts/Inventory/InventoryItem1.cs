using System;

namespace Project.Inventory
{
    public class InventoryItem : IInventoryItem
    {
        private Guid id = Guid.NewGuid();
        private string title;
        private byte stackAmound;

        public Guid Id { get => id; private set => id = value; }
        public string Title { get => title; private set => title = value; }
        public byte StackAmound { get => stackAmound; }
    }
}