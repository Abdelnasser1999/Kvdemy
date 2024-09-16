using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Transactions;

namespace Kvdemy.Core.ViewModels
{
    public class FinanceAccountViewModel
    {
		public int Id { get; set; }

		[JsonIgnore]
        public UserViewModel User { get; set; }
        public double? Balance { get; set; }

        public List<TransactionsViewModel> AccountTransactions { get; set; }

		public DateTime CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string UpdatedBy { get; set; }

	}
}
