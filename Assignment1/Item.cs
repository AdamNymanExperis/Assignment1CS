namespace Assignment1
{
    public abstract class Item
    {
        public string Name { get; set; } = "Item";
        public int RequiredLevel { get; set; } = 1;
        public Slots Slot { get; set; }

    }

    public enum Slots { 
        Weapon,
        Head,
        Body,
        Legs
    }
}