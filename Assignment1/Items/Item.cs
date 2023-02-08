namespace Assignment1.Items
{
    public abstract class Item
    {
        public string Name { get; set; } = "Item";
        public int RequiredLevel { get; set; } = 1;
        public Enums.Slot Slot { get; set; }

    }
}