using Assignment1.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPack.ItemCreators
{
    public interface IWeaponCreator
    {
        public Weapon CreateWeapon(int level);
    }
}
