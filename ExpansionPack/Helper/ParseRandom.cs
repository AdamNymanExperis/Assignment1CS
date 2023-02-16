using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPack.Helper
{
    // The reasoning with this class is to take out Random() from the classes to make it possible to mock random in testing 
    public class ParseRandom : IRandom
    {
        public int Next(int maxValue)
        {
            return new Random().Next(maxValue);
        }
    }
}
