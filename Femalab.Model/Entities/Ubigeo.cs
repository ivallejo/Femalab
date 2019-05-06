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
    [Table("Ubigeo")]
    public class Ubigeo : AuditableEntity<long>
    {
        [Required]
        [MaxLength(6)]
        public string Code { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

    }
}
