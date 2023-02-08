using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assignment1.Helper;
using Assignment1.Items;
using Assignment1.Enums;
using Assignment1.Exceptions;

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
            if (isRequiredLevelForArmor(armor.RequiredLevel) && isValidArmorType(armor.ArmorType))
            {
                Equipment.Remove(armor.Slot);
                Equipment.Add(armor.Slot, armor);
            }
        }

        public void Equip(Weapon weapon)
        {
            if (isRequiredLevelForWeapon(weapon.RequiredLevel) && isValidWeaponType(weapon.WeaponType))
            {
                Equipment.Remove(weapon.Slot);
                Equipment.Add(weapon.Slot, weapon);
            }
        }
        private bool isRequiredLevelForArmor(int requiredLevel)
        {
            if (requiredLevel <= Level) return true;
            else throw new InvalidArmorException($"You require a higher level to equip that armor! (required level {requiredLevel})");
        }
        private bool isValidArmorType(ArmorType type)
        {
            if (ValidArmorTypes.Contains(type)) return true;
            else throw new InvalidArmorException($"You can't wear armors of that type! ({type})");
        }
        private bool isRequiredLevelForWeapon(int requiredLevel)
        {
            if (requiredLevel <= Level) return true;
            else throw new InvalidWeaponException($"You require a higher level to equip that weapon! (required level {requiredLevel})");
        }
        private bool isValidWeaponType(WeaponType type)
        {
            if (ValidWeaponTypes.Contains(type)) return true;
            else throw new InvalidWeaponException($"You can't use weapons of that type! ({type})"); ;
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

        protected int getTotalStrength()
        {
            var attributes = TotalAttributes().GetAttributes();
            if(attributes.TryGetValue(AttributeType.Strength, out int strength)) return strength;
            else return 0;
        }
        protected int getTotalDexterity() {
            var attributes = TotalAttributes().GetAttributes();
            if (attributes.TryGetValue(AttributeType.Dexterity, out int dexterity)) return dexterity;
            else return 0;
        }
        protected int getTotalIntelligence()
        {
            var attributes = TotalAttributes().GetAttributes();
            if (attributes.TryGetValue(AttributeType.Intelligence, out int intelligence)) return intelligence;
            else return 0;
        }

        public void Display()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"---{Name}---");
            //sb.AppendLine($"Class: {}");
            sb.AppendLine($"Level: {Level})");
        }
    }
}
