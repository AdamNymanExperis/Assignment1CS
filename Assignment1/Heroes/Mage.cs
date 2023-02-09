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
    public class Mage : Hero
    {
        public Mage(string name) : base(name)
        {
            Class = "Mage";
            ValidArmorTypes.Add(ArmorType.Cloth);
            ValidWeaponTypes.AddRange(new List<WeaponType>{ WeaponType.Wands, WeaponType.Staffs});
            LevelAttributes = new HeroAttribute(1, 1, 8);
        }

        public override void LevelUp()
        {
            Level++;
            LevelAttributes += new HeroAttribute(1, 1, 5);
        }
        public override int Damage()
        {
            var attributes = TotalAttributes().GetAttributes();
            int weaponDamage = 1;

            if (Equipment.TryGetValue(Slot.Weapon, out Item item))
            {
                Weapon weapon = item as Weapon;
                if (weapon != null)
                {
                    weaponDamage = weapon.WeaponDamage;
                }
            }

            return weaponDamage * (1 + getTotalIntelligence()/ 100);
        }
    }
}
