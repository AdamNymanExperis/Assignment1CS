namespace Assignment1
{
    public class Armor : Item
    {
        public HeroAttribute ArmorAttribute = new HeroAttribute();
        public ArmorTypes ArmorType { get; set; }
    }

    public enum ArmorTypes { 
        Cloth,
        Leather,
        Mail,
        Plate
    }
}