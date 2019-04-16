using Femalab.Model.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Femalab.Model.Entities
{
    [Table("Category")]
    public class Category : AuditableEntity<long>
    {

        [MaxLength(10)]
        public string Code { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string Observation { get; set; }

        public bool State { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
