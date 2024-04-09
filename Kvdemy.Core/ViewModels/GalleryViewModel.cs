using Kvdemy.Core.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class GalleryViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string UserId { get; set; }
    }
}
