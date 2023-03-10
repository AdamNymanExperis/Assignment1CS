using Assignment1.Enums;
using Assignment1.Helper;
using Assignment1.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Heroes
{
    public class Rogue : Hero
    {
        public Rogue(string name) : base(name)
        {
            Class = "Rogue";
            ValidArmorTypes.AddRange(new List<ArmorType> { ArmorType.Leather, ArmorType.Mail });
            ValidWeaponTypes.AddRange(new List<WeaponType> { WeaponType.Daggers, WeaponType.Swords });
            LevelAttributes = new HeroAttribute(2, 6, 1);
        }

        public override void LevelUp()
        {
            Level++;
            LevelAttributes += new HeroAttribute(1, 4, 1);
        }
        public override int Damage()
        {
            return GetWeaponDamage() * (1 + GetTotalDexterity()/ 100);
        }
    }
}
