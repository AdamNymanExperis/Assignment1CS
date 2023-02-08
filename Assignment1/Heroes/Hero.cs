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
        public HeroAttribute LevelAttributes { get; set; } = new HeroAttribute();
        public Dictionary<Enums.Slot, Item> Equipment = new Dictionary<Enums.Slot, Item>();
        public List<WeaponType> ValidWeaponTypes = new List<WeaponType>();
        public List<ArmorType> ValidArmorTypes = new List<ArmorType>();

        public abstract void LevelUp();
        public abstract int Damage();

        public void Equip(Armor armor) 
        {
            if (isRequiredLevel(armor.RequiredLevel) && isValidArmorType(armor.ArmorType))
            {
                Equipment.Remove(armor.Slot);
                Equipment.Add(armor.Slot, armor);
            }
        }

        public void Equip(Weapon weapon)
        {
            if (isRequiredLevel(weapon.RequiredLevel) && isValidWeaponType(weapon.WeaponType))
            {
                Equipment.Remove(weapon.Slot);
                Equipment.Add(weapon.Slot, weapon);
            }
        }
        private bool isRequiredLevel(int requiredLevel)
        {
            if (requiredLevel <= Level) return true;
            else throw new Exception();
        }
        private bool isValidArmorType(ArmorType type)
        {
            if (ValidArmorTypes.Contains(type)) return true;
            else throw new Exception();
        }

        private bool isValidWeaponType(WeaponType type)
        {
            if (ValidWeaponTypes.Contains(type)) return true;
            else throw new Exception();
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
