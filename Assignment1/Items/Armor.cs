using Assignment1.Enums;
using Assignment1.Helper;
using System.Xml.Linq;

namespace Assignment1.Items
{
    public class Armor : Item
    {
        public HeroAttribute ArmorAttribute { get; private set; }
        public ArmorType ArmorType { get; private set; }

        public Armor() {
            this.Name = "Gandalf's Wizard Hat";
            this.RequiredLevel = 95;
            this.Slot = Slot.Head;
            this.ArmorType = ArmorType.Cloth;
            this.ArmorAttribute = new HeroAttribute(0,0,35);
        }

        public Armor(string name, int requiredLevel, Slot slot, ArmorType armorType, HeroAttribute armorAttribute) {
            this.Name = name;
            this.RequiredLevel = requiredLevel;
            this.Slot = slot;
            this.ArmorType = armorType;  
            this.ArmorAttribute = armorAttribute; 
        }
    }

    
}