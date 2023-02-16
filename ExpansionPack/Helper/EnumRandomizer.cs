using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPack.Helper
{
    public class EnumRandomizer : IEnumRandomizer
    {
        private IRandom random;

        public EnumRandomizer()
        {
            random = new ParseRandom();        
        }
        public EnumRandomizer(IRandom aRandom) 
        {
            random = aRandom;
        }
        public Enum RandomEnum(Enum aEnum)
        {
            Array values = Enum.GetValues(aEnum.GetType());
            var randomValue = (Enum)values.GetValue(random.Next(values.Length));

            return randomValue;
        }
    }
}
