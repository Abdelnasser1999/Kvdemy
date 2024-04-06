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
    public class EducationViewModel
    {
        public int Id { get; set; }
        public string InstituteName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DegreeTitle { get; set; }
        public string? Description { get; set; }

        public int UserId { get; set; }

    }
}
