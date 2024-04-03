using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class JsonResponseViewModel
    {
        public bool status { get; set; }
        public int close { get; set; }
        public string msg { get; set; }
    }
}
