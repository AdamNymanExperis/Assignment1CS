namespace Assignment1
{
    public class Item
    {
        public Slot Slot { get; set; }
    }

    public enum Slot { 
        Weapon,
        Head,
        Body,
        Legs
    }
}