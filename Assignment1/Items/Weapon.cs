using Assignment1.Enums;
using Assignment1.Helper;

namespace Assignment1.Items
{
    public class Weapon : Item
    {
        public int WeaponDamage { get; private set; }
        public WeaponType WeaponType { get; private set; }



        public Weapon()
        {
            this.Name = "Sting, the Sword of Bilbo Baggins";
            this.RequiredLevel = 3;
            this.Slot = Slot.Weapon;
            this.WeaponType = WeaponType.Swords;
            this.WeaponDamage = 4;
        }

        public Weapon(string name, int requiredLevel, Slot slot, WeaponType weaponType, int damage)
        {
            this.Name = name;
            this.RequiredLevel = requiredLevel;
            this.Slot = slot;
            this.WeaponType = weaponType;
            this.WeaponDamage = damage;
        }
    }
}