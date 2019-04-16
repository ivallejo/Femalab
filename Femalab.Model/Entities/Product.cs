using Femalab.Model.Audit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Femalab.Model.Entities
{
    [Table("Product")]
    public class Product : AuditableEntity<long>
    {
        [MaxLength(10)]
        public string Code { get; set; }

        public long CategoryId { get; set; }
        public long? SpecialtyId { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        public decimal StockInitial { get; set; }
        [MaxLength(500)]
        public string Observation { get; set; }

        public bool State { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("SpecialtyId")]
        public Specialty Specialty { get; set; }

        public ICollection<AttentionDetails> AttentionDetails { get; set; }
    }
}
