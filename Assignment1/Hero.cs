using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public abstract class Hero
    {
        public Hero(string name) {
            Name = name;
        }
        public string Name { get; set; }
        public int Level { get; set; } = 1;
        public HeroAttribute LevelAttributes { get; } = new HeroAttribute();
        public List<Item> Equipment = new List<Item>();
        public object ValidWeaponTypes = new List<String>();
        public object ValidArmorTypes = new List<String>();

        public virtual void LevelUp()
        {
            Level++; 
        }

        public abstract void Equip(Armor armor);
        public abstract void Equip(Weapon weapon);
        public abstract void Damage(); 

        public HeroAttribute TotalAttributes() 
        {
            var total = new HeroAttribute() + LevelAttributes;
            for (int i = 0; i < Equipment.Count(); i++) 
            {
                if (Equipment[i].Slot != Slot.Weapon)
                {
                    var armorAttributes = Equipment[i].ArmorAttribute;
                    total += new HeroAttribute(armorAttributes[0], armorAttributes[1], armorAttributes[2]);
                }
            }
            return total;
        }

        public void Display() 
        { 
            
        }
    }
}
