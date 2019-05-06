using Femalab.Model.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Model.Entities
{
    [Table("Payment")]
    public class Payment : AuditableEntity<long>
    {
        public long InvoiceId { get; set; }
        [MaxLength(1)]
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public bool State { get; set; }
        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }
    }
}
