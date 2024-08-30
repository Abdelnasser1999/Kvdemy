using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class WalletRequestViewModel
    {

        public int Id { get; set; }
        public double Amount { get; set; }
        public WalletRequestStatus Status { get; set; }
        public WalletRequestPaymentType Type { get; set; }
        public string? Attachment { get; set; }

        public string UserId { get; set; }
        public UserViewModel User { get; set; }

    }
}
