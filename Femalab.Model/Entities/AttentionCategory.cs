using Femalab.Model.Audit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Femalab.Model.Entities
{
    [Table("AttentionCategory")]
    public class AttentionCategory : AuditableEntity<long>
    {
        [MaxLength(10)]
        public string Code { get; set; }
        [MaxLength(150)]
        public string Category { get; set; }
        [MaxLength(30)]
        public string Tag { get; set; }
        [MaxLength(100)]
        public string Action { get; set; }
        public bool State { get; set; }
        public ICollection<Attention> Attentions { get; set; }
    }
}
