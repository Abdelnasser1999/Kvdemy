using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class ApiResponseSuccessViewModel
    {

        public bool Status { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }


        public ApiResponseSuccessViewModel(string message, object data)
        {
            Status = true;
            Msg = message;
            Data = data;



        }
        public ApiResponseSuccessViewModel(string message)
        {
            Status = true;
            Msg = message;
        }
    }
}
