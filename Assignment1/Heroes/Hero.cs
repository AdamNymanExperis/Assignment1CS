using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assignment1.Helper;
using Assignment1.Items;
using Assignment1.Enums;

namespace Assignment1.Heroes
{
    public abstract class Hero
    {
        public Hero(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public int Level { get; set; } = 1;
        public HeroAttribute LevelAttributes { get; } = new HeroAttribute();
        public Dictionary<Enums.Slot, Item> Equipment = new Dictionary<Enums.Slot, Item>();
        public List<WeaponType> ValidWeaponTypes = new List<WeaponType>();
        public List<ArmorType> ValidArmorTypes = new List<ArmorType>();

        public abstract void LevelUp();
        public abstract void Damage();

        public void Equip(Armor armor) 
        {
            if (armor.RequiredLevel <= Level && ValidArmorTypes.Contains(armor.ArmorType))
            {
                Equipment.Remove(armor.Slot);
                Equipment.Add(armor.Slot, armor);
            }
            else {
                
            }
        }
        public void Equip(Weapon weapon)
        {
            if (weapon.RequiredLevel <= Level && ValidWeaponTypes.Contains(weapon.WeaponType))
            {
                Equipment.Remove(weapon.Slot);
                Equipment.Add(weapon.Slot, weapon);
            }
        }

        public HeroAttribute TotalAttributes()
        {
            var total = new HeroAttribute() + LevelAttributes;

            var allArmors = Equipment.OfType<Armor>();

            foreach (Armor armor in allArmors)
            {
                total += armor.ArmorAttribute;
            }

            return total;
        }

        public void Display()
        {

        }
    }
}
