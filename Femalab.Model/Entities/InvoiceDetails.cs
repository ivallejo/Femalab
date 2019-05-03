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
   [Table("InvoiceDetails")]
    public class InvoiceDetails : AuditableEntity<long>
    {

        public long InvoiceId { get; set; }
        public long ProductId { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
