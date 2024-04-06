using Krooti.Core.Enums;
using Kvdemy.Core.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krooti.Core.ViewModels
{
    public class VideoViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
    }
}
