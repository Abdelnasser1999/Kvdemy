﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Dtos.Helpers
{
   public class Pagination
    {
        public int PerPage { get; set; } 
        public int Page { get; set; } 
        public int Pages { get; set; }
        public int Total { get; set; }
    }
}
