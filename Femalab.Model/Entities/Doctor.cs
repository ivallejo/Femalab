using Femalab.Model.Audit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Femalab.Model.Entities
{
    [Table("Doctor")]
    public class Doctor : AuditableEntity<long>
    {
        [MaxLength(10)]
        public string Code { get; set; }
        [MaxLength(250)]
        public string FullName { get; set; }
        public bool State { get; set; }
        public ICollection<Attention> Attentions { get; set; }
    }
}
