using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Exceptions
{
    public class NationalityNotFoundException : Exception
    {
        public NationalityNotFoundException() : base("المدينة غير موجودة")
        {

        }
    }
}
