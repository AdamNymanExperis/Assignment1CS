using Assignment1.Enums;
using Assignment1.Helper;
using Assignment1.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Heroes
{
    public class Warrior : Hero
    {
        public Warrior(string name) : base(name)
        {
            Class = "Warrior";
            ValidArmorTypes.AddRange(new List<ArmorType> { ArmorType.Plate, ArmorType.Mail });
            ValidWeaponTypes.AddRange(new List<WeaponType> { WeaponType.Hammers, WeaponType.Axes, WeaponType.Swords});
            LevelAttributes = new HeroAttribute(5, 2, 1);
        }

        public override void LevelUp()
        {
            Level++;
            LevelAttributes += new HeroAttribute(3, 2, 1);
        }
        public override int Damage()
        {
            return GetWeaponDamage() * (1 + GetTotalStrength()/ 100);
        }
    }
}
