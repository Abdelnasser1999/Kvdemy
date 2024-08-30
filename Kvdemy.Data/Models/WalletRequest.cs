using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class WalletRequest : BaseEntity
    {

        public int Id { get; set; }

        public double Amount { get; set; }
        public WalletRequestStatus Status { get; set; }
        public WalletRequestPaymentType Type { get; set; }
        public string? Attachment { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public WalletRequest() 
        { 
            Status = WalletRequestStatus.Pending;
            Type = WalletRequestPaymentType.Cash;
        }
    }
}
