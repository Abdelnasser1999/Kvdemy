using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kvdemy.Core.ViewModels
{
    public class ApiResponseFailedViewModel
    {
        public bool Status { get; set; }
        public string Msg { get; set; }
       

        public ApiResponseFailedViewModel(string message)
        {
            Status = false;
            Msg = message;
      



        }
    }
}
