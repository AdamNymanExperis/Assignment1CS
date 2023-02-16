using ExpansionPack.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPack.Helper
{
    public class RarityHandler : IRarityHandler
    {
        private readonly IEnumRandomizer enumRandomizer;
        private readonly IRandom random;
        public Rarity Rarity { get; private set; }

        public RarityHandler() 
        {
            enumRandomizer = new EnumRandomizer();
            random = new ParseRandom();
        }
        public RarityHandler(IEnumRandomizer aRandomizer, IRandom aRandom)
        {
            enumRandomizer = aRandomizer;
            random = aRandom;
        }
        public void RandomizeRarity()
        {
            var randomNumber = random.Next(100);

            if (randomNumber < 1) Rarity =  Rarity.Legendary;       // 1 / 100 is Legendary 
            else if (randomNumber < 11) Rarity = Rarity.Epic;       // 10 / 100 is Epic
            else if (randomNumber < 31) Rarity = Rarity.Rare;       // 20 / 100 is Rare
            else Rarity = Rarity.Common;                            // rest is Common
        }
        public string GetRarityPrefix()
        {
            if (Rarity == Rarity.Legendary) return "Legendary";
            else if (Rarity == Rarity.Epic) return enumRandomizer.RandomEnum(EpicPrefix.Epic).ToString();
            else if (Rarity == Rarity.Rare) return enumRandomizer.RandomEnum(RarePrefix.Rare).ToString();
            else return enumRandomizer.RandomEnum(CommonPrefix.Common).ToString();
        }
        public int GetRarityBonus()
        {
            if (Rarity == Rarity.Legendary) return 7;
            else if (Rarity == Rarity.Epic) return 5;
            else if (Rarity == Rarity.Rare) return 3;
            else return 0;
        }
    }
}
