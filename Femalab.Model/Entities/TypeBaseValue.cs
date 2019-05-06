using Femalab.Model.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Model.Entities
{
    public class TypeBaseValue : AuditableEntity<long>
    {
        [Required]
        [StringLength(5)]
        public string Code { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public string CompleteDescription => $"{Code}: {Description}";
    }
}
