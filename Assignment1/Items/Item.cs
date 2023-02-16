using Assignment1.Enums;

namespace Assignment1.Items
{
    public abstract class Item
    {
        public string Name { get; protected set; } = "Item";
        public int RequiredLevel { get; protected set; } = 1;
        public Slot Slot { get; protected set; }
    }
}