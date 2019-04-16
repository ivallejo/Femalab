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
    [Table("Patient")]
    public class Patient : AuditableEntity<long>
    {

        [MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(250)]
        public string FirstName { get; set; }

        [MaxLength(250)]
        public string LastName { get; set; }
        [MaxLength(2)]
        public string DocumentType { get; set; }
        [MaxLength(20)]
        public string Document { get; set; }
        [MaxLength(1)]
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [MaxLength(550)]
        public string Address { get; set; }
        [MaxLength(250)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(550)]
        public string Observation { get; set; }

        public bool State { get; set; }

        public ICollection<Attention> Attentions { get; set; }
    }
}
