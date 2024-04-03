using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Exceptions
{
    public class CouponNotFoundException : Exception
    {

        public CouponNotFoundException() : base("الكوبون غير موجود")
        { 
        }
    }
}
