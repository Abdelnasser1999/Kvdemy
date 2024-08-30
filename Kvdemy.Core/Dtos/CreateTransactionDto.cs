using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Dtos
{
    public class CreateTransactionDto
    {

 
        public int FinanceAccountId { get; set; }

        public TransactionType TransactionType { get; set; }

        public double ValueBeforDiscount { get; set; }
        public double ValueAfterDiscount { get; set; }
        public double ValueDiscount { get; set; }

        public TransactionPaymentType TransactionPaymentType { get; set; }


    }
}
