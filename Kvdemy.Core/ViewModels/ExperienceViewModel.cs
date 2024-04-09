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
    public class ExperienceViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string JobTitle { get; set; }
        public string? Description { get; set; }
        public string UserId { get; set; }

    }
}
