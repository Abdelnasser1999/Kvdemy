using Kvdemy.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class FinanceAccount : BaseEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public double? Balance { get; set; }

    
        public List<AccountTransactions> AccountTransactions { get; set; }
    
    }
}
