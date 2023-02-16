using Assignment1.Enums;
using Assignment1.Helper;
using Assignment1.Items;
using ExpansionPack.Enums;
using ExpansionPack.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPack.ItemCreators
{
    public class RandomArmorCreator : IArmorCreator
    {
        private readonly IRarityHandler rarityHandler;
        private readonly IEnumRandomizer enumRandomizer;
        private readonly IRandom random; 

        public RandomArmorCreator() 
        {
            rarityHandler = new RarityHandler();
            enumRandomizer = new EnumRandomizer();
            random = new ParseRandom();
        }

        public RandomArmorCreator(IRarityHandler aRarityHandler, IEnumRandomizer aEnumRandomizer, IRandom aRandom)
        {
            rarityHandler = aRarityHandler;
            enumRandomizer = aEnumRandomizer;
            random = aRandom;
        }

        public Armor CreateArmor(int itemLevel, Slot itemSlot)
        {
            rarityHandler.RandomizeRarity();
            var itemArmorType = (ArmorType)enumRandomizer.RandomEnum(ArmorType.Leather);
            var itemName = GetArmorName(itemArmorType, itemSlot);
            var itemAttribute = GetArmorAttributes(itemLevel, itemArmorType);

            return new Armor(itemName, itemLevel, itemSlot, itemArmorType, itemAttribute);
        }

        private string GetArmorName(ArmorType itemArmorType, Slot itemSlot)
        {
            var prefix = rarityHandler.GetRarityPrefix();

            string itemName = "";
            if (itemSlot == Slot.Head) itemName = "Helmet";
            if (itemSlot == Slot.Body) itemName = "Armor";
            if (itemSlot == Slot.Legs) itemName = "Boots";

            return $"{prefix} {itemArmorType} {itemName}";
        }

        private HeroAttribute GetArmorAttributes(int itemLevel, ArmorType itemArmorType)
        {
            var rarityAttribute = rarityHandler.GetRarityBonus();

            // adds extra attribute based on Level and random factor
            var weakAttribute = itemLevel + rarityAttribute + random.Next(3);                                  
            var strongAttribute = itemLevel + rarityAttribute + random.Next(3) + 8;

            HeroAttribute itemAttribute = new HeroAttribute(0, 0, 0);
            // decides strong and weak attributes based on armortype. for ex. Cloth is mage armor, meaning that it gives stronger intelligence
            if (itemArmorType == ArmorType.Cloth) itemAttribute = new HeroAttribute(weakAttribute, weakAttribute, strongAttribute);     
            if (itemArmorType == ArmorType.Leather) itemAttribute = new HeroAttribute(weakAttribute, strongAttribute, weakAttribute);   
            if (itemArmorType == ArmorType.Mail) itemAttribute = new HeroAttribute(weakAttribute, strongAttribute, weakAttribute);
            if (itemArmorType == ArmorType.Plate) itemAttribute = new HeroAttribute(strongAttribute, weakAttribute, weakAttribute);

            return itemAttribute;
        }
    }
}
