using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public abstract class Hero
    {
        public Hero(string name) {
            Name = name;
        }
        public string Name { get; set; }
        public int Level { get; } = 1;
        public List<Item> Equipment = new List<Item>();
        public object ValidWeaponTypes = new List<String>();
        public object ValidArmorTypes = new List<String>();
    }
}
