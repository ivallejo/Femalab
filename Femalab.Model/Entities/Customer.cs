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
    [Table("Customer")]
    public class Customer : AuditableEntity<long>
    {
        [MaxLength(2)]
        public string DocumentType { get; set; }
        [MaxLength(20)]
        public string Document { get; set; }
        [MaxLength(250)]
        public string FirstName { get; set; }
        [MaxLength(250)]
        public string TradeName { get; set; }
        [MaxLength(2)]
        public string Country { get; set; }
        [MaxLength(2)]
        public string Department { get; set; }
        [MaxLength(2)]
        public string Province { get; set; }
        [MaxLength(2)]
        public string District { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(250)]
        public string Email { get; set; }
        [MaxLength(4)]
        public string DomicileFiscalCode { get; set; }
    }
}
