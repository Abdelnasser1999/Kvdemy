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
    public class UserSpecialtyViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public SpecialtyViewModel Specialty { get; set; }
        public ServiceViewModel Service { get; set; }
        public float Price { get; set; }
        public string? Description { get; set; }
    }
}
