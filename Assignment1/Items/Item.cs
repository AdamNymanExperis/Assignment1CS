using Assignment1.Enums;

namespace Assignment1.Items
{
    public abstract class Item
    {
        protected string Name { get; set; } = "Item";
        public int RequiredLevel { get; protected set; } = 1;
        public Slot Slot { get; protected set; }

    }
}