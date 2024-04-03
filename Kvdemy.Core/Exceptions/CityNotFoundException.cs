using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Exceptions
{
    public class CityNotFoundException : Exception
    {
        public CityNotFoundException() : base("المدينة غير موجودة")
        {

        }
    }
}
