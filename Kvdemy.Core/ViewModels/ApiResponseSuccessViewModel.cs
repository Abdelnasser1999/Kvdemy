using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class ApiResponseSuccessNoDataViewModel
    {

        public bool Status { get; set; }
        public string Msg { get; set; }



        public ApiResponseSuccessNoDataViewModel(string message)
        {
            Status = true;
            Msg = message;
           



        }
    }
}
