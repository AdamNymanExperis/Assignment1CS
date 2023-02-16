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
        public string Name { get; protected set; }
        public int Level { get; protected set; } = 1;
        public HeroAttribute LevelAttributes { get; protected set; } = new HeroAttribute();
        public Dictionary<Enums.Slot, Item> Equipment = new Dictionary<Enums.Slot, Item>();
        protected List<WeaponType> ValidWeaponTypes = new List<WeaponType>();
        protected List<ArmorType> ValidArmorTypes = new List<ArmorType>();

        public Item[] GetEquipment()
        {
            return Equipment.Values.ToArray();
        }
        protected string Class { get; set; } = "none";

        public abstract void LevelUp();

        public void Equip(Armor armor) 
        {
            if (IsRequiredLevelForArmor(armor.RequiredLevel) && IsValidArmorType(armor.ArmorType))
            {
                Equipment.Remove(armor.Slot);
                Equipment.Add(armor.Slot, armor);
            }
        }

        public void Equip(Weapon weapon)
        {
            if (IsRequiredLevelForWeapon(weapon.RequiredLevel) && IsValidWeaponType(weapon.WeaponType))
            {
                Equipment.Remove(weapon.Slot);
                Equipment.Add(weapon.Slot, weapon);
            }
        }
        private bool IsRequiredLevelForArmor(int requiredLevel)
        {
            if (requiredLevel <= Level) return true;
            else throw new InvalidArmorException($"You require a higher level to equip that armor! (required level {requiredLevel})");
        }
        private bool IsValidArmorType(ArmorType type)
        {
            if (ValidArmorTypes.Contains(type)) return true;
            else throw new InvalidArmorException($"You can't wear armors of that type! ({type})");
        }
        private bool IsRequiredLevelForWeapon(int requiredLevel)
        {
            if (requiredLevel <= Level) return true;
            else throw new InvalidWeaponException($"You require a higher level to equip that weapon! (required level {requiredLevel})");
        }
        private bool IsValidWeaponType(WeaponType type)
        {
            if (ValidWeaponTypes.Contains(type)) return true;
            else throw new InvalidWeaponException($"You can't use weapons of that type! ({type})"); ;
        }

        public HeroAttribute TotalAttributes()
        {
            var total = new HeroAttribute() + LevelAttributes;

            foreach (KeyValuePair<Slot, Item> Pair in Equipment) 
            {
                if (Pair.Value as Armor != null) 
                {
                    total += ((Armor)Pair.Value).ArmorAttribute;
                }
            }

            return total;
        }

        protected int GetTotalStrength()
        {
            var attributes = TotalAttributes().GetAttributes();
            if(attributes.TryGetValue(AttributeType.Strength, out int strength)) return strength;
            else return 0;
        }
        protected int GetTotalDexterity() {
            var attributes = TotalAttributes().GetAttributes();
            if (attributes.TryGetValue(AttributeType.Dexterity, out int dexterity)) return dexterity;
            else return 0;
        }
        protected int GetTotalIntelligence()
        {
            var attributes = TotalAttributes().GetAttributes();
            if (attributes.TryGetValue(AttributeType.Intelligence, out int intelligence)) return intelligence;
            else return 0;
        }

        public virtual int Damage() 
        {
            return 1;
        }

        protected int GetWeaponDamage() 
        {
            if (Equipment.TryGetValue(Slot.Weapon, out Item? item))
            {
                Weapon? weapon = item as Weapon;
                if (weapon != null)
                {
                    return weapon.WeaponDamage;
                }
            }
            return 1;
        }

        public string Display()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Name: {Name}");
            stringBuilder.AppendLine($"Class: {Class}");
            stringBuilder.AppendLine($"Level: {Level}");
            stringBuilder.AppendLine($"Total strength: {GetTotalStrength()}");
            stringBuilder.AppendLine($"Total dexterity: {GetTotalDexterity()}");
            stringBuilder.AppendLine($"Total intelligence: {GetTotalIntelligence()}");
            stringBuilder.AppendLine($"Damage: {Damage()}");
            return stringBuilder.ToString();
        }
    }
}
