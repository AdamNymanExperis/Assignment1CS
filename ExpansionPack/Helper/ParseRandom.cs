using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPack.Helper
{
    public class ParseRandom : IRandom
    {
        public int Next(int maxValue)
        {
            return new Random().Next(maxValue);
        }
    }
}
