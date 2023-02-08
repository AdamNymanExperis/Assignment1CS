using Assignment1.Helper;

namespace Assignment1.Items
{
    public class Armor : Item
    {
        public HeroAttribute ArmorAttribute = new HeroAttribute();
        public Enums.ArmorType ArmorType { get; set; }
    }

    
}