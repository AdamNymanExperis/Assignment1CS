namespace Assignment1
{
    public abstract class Item
    {
        public string Name { get; set; } = "Item";
        public int RequiredLevel { get; set; } = 1;
        public Slot Slot { get; set; }

    }

    public enum Slot { 
        Weapon,
        Head,
        Body,
        Legs
    }
}