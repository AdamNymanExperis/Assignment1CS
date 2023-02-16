using Assignment1.Enums;
using Assignment1.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPack.ItemCreators
{
    public interface IArmorCreator
    {
        public Armor CreateArmor(int itemLevel, Slot itemSlot);
    }
}
