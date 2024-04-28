using Kvdemy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class LanguageLevel : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StudentLanguage> StudentLanguages { get; set; }

    }
}
