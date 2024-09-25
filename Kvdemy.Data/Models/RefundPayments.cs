using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class RefundPayments : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string StudentId { get; set; }
        public User Student { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
