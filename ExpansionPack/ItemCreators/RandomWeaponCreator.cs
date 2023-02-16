using Assignment1.Enums;
using Assignment1.Items;
using ExpansionPack.Enums;
using ExpansionPack.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPack.ItemCreators
{
    public class RandomWeaponCreator : IWeaponCreator
    {
        private readonly IRarityHandler rarityHandler;
        private readonly IEnumRandomizer enumRandomizer;
        private readonly IRandom random;

        public RandomWeaponCreator() 
        {
            rarityHandler = new RarityHandler();
            enumRandomizer= new EnumRandomizer();
            random = new ParseRandom();
        }

        public RandomWeaponCreator(IRarityHandler aRarityHandler, IEnumRandomizer aEnumRandomizer, IRandom aRandom)
        {
            rarityHandler = aRarityHandler;
            enumRandomizer = aEnumRandomizer;
            random = aRandom;
        }

        public Weapon CreateWeapon(int itemLevel)
        {
            rarityHandler.RandomizeRarity();
            var itemWeaponType = (WeaponType)enumRandomizer.RandomEnum(WeaponType.Swords);
            var itemName = GetWeaponName(itemWeaponType);
            var itemDamage = GetWeaponDamage(itemLevel);

            return new Weapon(itemName, itemLevel, itemWeaponType, itemDamage);
        }

        private string GetWeaponName(WeaponType itemWeaponType)
        {
            string prefix = rarityHandler.GetRarityPrefix();

            var material = (WeaponMaterial)enumRandomizer.RandomEnum(WeaponMaterial.Iron);

            var weaponName = $"{prefix} {material} {itemWeaponType}";
            weaponName = weaponName.Remove(weaponName.Length - 1, 1);

            return weaponName;
        }

        private int GetWeaponDamage(int itemLevel)
        {
            var rarityDmg = rarityHandler.GetRarityBonus();                    // adds extra attribute based on the items rarity
            return itemLevel - 5 + random.Next(3) + rarityDmg;
        }
    }
}
