using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assignment1.Helper;
using Assignment1.Items;

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
        public Dictionary<Slots, Item> Equipment = new Dictionary<Slots, Item>();
        public object ValidWeaponTypes = new List<string>();
        public object ValidArmorTypes = new List<string>();

        public void LevelUp()
        {
            Level++;
        }

        public abstract void Equip(Armor armor);
        public abstract void Equip(Weapon weapon);
        public abstract void Damage();

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
