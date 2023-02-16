using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPack.Helper
{
    public interface IRarityHandler
    {
        public void RandomizeRarity();
        public string GetRarityPrefix();
        public int GetRarityBonus();
    }
}
