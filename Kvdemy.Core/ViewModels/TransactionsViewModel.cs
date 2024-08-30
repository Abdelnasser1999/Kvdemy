using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class TransactionsViewModel
    {
        public int Id { get; set; }
        public int FinanceAccountId { get; set; }

        public FinanceAccountViewModel FinanceAccount { get; set; }

        public TransactionType TransactionType { get; set; }

        public double ValueBeforDiscount { get; set; }
        public double ValueAfterDiscount { get; set; }
        public double ValueDiscount { get; set; }

        public TransactionPaymentType TransactionPaymentType { get; set; }

		public DateTime CreatedAt { get; set; }
		public string CreatedBy { get; set; }


	}
}
