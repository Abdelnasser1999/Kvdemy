using Kvdemy.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class TeacherSearchViewModel
    {
        public string Id { get; set; }
        public string? NameBase { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfileImage { get; set; }
        public string? Description { get; set; }
        public float? StartingPrice { get; set; }
        public float? Rating { get; set; }
        public int? RatingSum { get; set; }
        public int? RatingCount { get; set; }
        public Gender Gender { get; set; }
        public int NationalityId { get; set; }
        public NationalityViewModel Nationality { get; set; }

    }
}
