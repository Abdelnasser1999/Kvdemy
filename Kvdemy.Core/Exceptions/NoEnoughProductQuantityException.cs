using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Exceptions
{
    public class NoEnoughProductQuantityException : Exception
    {
        public NoEnoughProductQuantityException() : base("الكمية غير متوفرة حاليا")
        {

        }
    }
}
