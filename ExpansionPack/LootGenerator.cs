using Assignment1.Enums;
using Assignment1.Helper;
using Assignment1.Items;
using Castle.Core.Logging;
using ExpansionPack.Enums;
using ExpansionPack.Helper;
using ExpansionPack.ItemCreators;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPack
{
    public class LootGenerator
    {
        private IArmorCreator armorCreator;
        private IWeaponCreator weaponCreator;
        private IEnumRandomizer randomizer;
        private IRandom random;

        public LootGenerator() 
        {
            armorCreator= new RandomArmorCreator();
            weaponCreator= new RandomWeaponCreator();
            randomizer= new EnumRandomizer();
            random = new ParseRandom();
        }
        public LootGenerator(IArmorCreator aArmorCreator, IWeaponCreator aWeaponCreator, IEnumRandomizer aEnumRandomizer, IRandom aRandom)
        {
            armorCreator = aArmorCreator;
            weaponCreator = aWeaponCreator;
            randomizer = aEnumRandomizer;
            random = aRandom;
        }
        
        //Get a random Weapon or random Armor
        public Item GetRandomLoot(int level) 
        {
            Item item;
            var itemLevel = GetItemLevel(level);
            var randomSlot = (Slot)randomizer.RandomEnum(Slot.Head);

            if (randomSlot == Slot.Weapon) item = weaponCreator.CreateWeapon(itemLevel);
            else item = armorCreator.CreateArmor(itemLevel, randomSlot);

            return item;
        }

        public Weapon GetRandomWeapon(int level)
        {
            var itemLevel = GetItemLevel(level);
            return weaponCreator.CreateWeapon(itemLevel);
        }

        public Armor GetRandomHeadwear(int level)
        {
            var itemLevel = GetItemLevel(level);
            return armorCreator.CreateArmor(itemLevel, Slot.Head);
        }

        public Armor GetRandomBodyPiece(int level)
        {
            var itemLevel = GetItemLevel(level);
            return armorCreator.CreateArmor(itemLevel, Slot.Body);
        }

        public Armor GetRandomBoots(int level)
        {
            var itemLevel = GetItemLevel(level);
            return armorCreator.CreateArmor(itemLevel, Slot.Legs);
        }

        // Generates a random level based on the players level when obtaining the item
        private int GetItemLevel(int playerLevel)
        {
            return playerLevel + random.Next(5) - 2;        
        }
    }
}
