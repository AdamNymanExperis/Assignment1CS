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
    public class Ranger : Hero
    {
        public Ranger(string name) : base(name)
        {
            Class = "Ranger";
            ValidArmorTypes.AddRange(new List<ArmorType> { ArmorType.Leather, ArmorType.Mail });
            ValidWeaponTypes.Add(WeaponType.Bows);
            LevelAttributes = new HeroAttribute(1, 7, 1);
        }

        public override void LevelUp()
        {
            Level++;
            LevelAttributes += new HeroAttribute(1, 5, 1);
        }
        public override int Damage()
        {
            return GetWeaponDamage() * (1 + GetTotalDexterity() / 100);
        }
    }
}
