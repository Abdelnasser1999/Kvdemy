using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class AccessTokenViewModel
    {
        public string AcessToken { get; set; }

        public DateTime ExpireAt { get; set; }
    }
}
