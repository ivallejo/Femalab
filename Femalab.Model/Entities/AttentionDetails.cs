using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Femalab.Model.Audit;

namespace Femalab.Model.Entities
{
    [Table("AttentionDetails")]
    public class AttentionDetails : AuditableEntity<long>
    {
        public long AttentionId { get; set; }
        public long ProductId { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("AttentionId")]
        public Attention Attention { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
