using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Exceptions
{
    internal class InvalidArmorException : Exception
    {  
        public InvalidArmorException() { }
        public InvalidArmorException(string message) : base(message){ }

        public override string Message => "InvalidArmorException";
    }
}
