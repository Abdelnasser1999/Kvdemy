using System.Collections.Generic;

namespace Kvdemy.Core.Dtos.Helpers
{
       public class PaginationWebViewModel
    {
        public Meta meta { get; set; }
        public object data { get; set; }
    }
        public class Meta
    {
        public int page { get; set; }
        public int pages { get; set; }
        public int perpage { get; set; }
        public int total { get; set; }
        public string sort { get; set; }
        public string field { get; set; }
    }

}
