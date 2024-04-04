using Krooti.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class StudentLanguage : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public int LanguageLevelId { get; set; }
        public LanguageLevel LanguageLevel { get; set; }

    }
}
