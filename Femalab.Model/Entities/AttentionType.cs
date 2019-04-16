using Femalab.Model.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Model.Entities
{
    [Table("AttentionType")]
    public class AttentionType : AuditableEntity<long>
    {
        [MaxLength(10)]
        public string Code { get; set; }
        [MaxLength(250)]
        public string Type { get; set; }
        [MaxLength(30)]
        public string Tag { get; set; }
        public bool State { get; set; }
        public ICollection<Attention> Attentions { get; set; }
    }
}
