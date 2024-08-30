using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class AccountTransactions : BaseEntity
    {
        public int Id { get; set; }
        public int FinanceAccountId { get; set; }

        public FinanceAccount FinanceAccount { get; set; }

        public TransactionType TransactionType { get; set; }

        public double ValueBeforDiscount { get; set; }
        public double ValueAfterDiscount { get; set; }
        public double ValueDiscount { get; set; }

        public TransactionPaymentType TransactionPaymentType { get; set; }

    }
}
