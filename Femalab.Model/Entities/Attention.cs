using Femalab.Model.Audit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Femalab.Model.Entities
{
    [Table("Attention")]
    public class Attention : AuditableEntity<long>
    {

        [MaxLength(10)]
        public string Code { get; set; }

        public int Age { get; set; }
        public int Visits { get; set; }
        public decimal Weight { get; set; }
        public decimal Size { get; set; }
        public decimal Height { get; set; }
        [DataType(DataType.MultilineText)]
        public string QueryBy { get; set; }
        [DataType(DataType.MultilineText)]
        public string PhysicalExam { get; set; }
        [DataType(DataType.MultilineText)]
        public string LaboratoryExam { get; set; }
        [DataType(DataType.MultilineText)]
        public string FamilyHistory { get; set; }
        [DataType(DataType.MultilineText)]
        public string Diagnosis { get; set; }
        [DataType(DataType.MultilineText)]
        public string Treatment { get; set; }
        public bool State { get; set; }
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
        public long AttentionTypeId { get; set; }
        public long AttentionCategoryId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        [ForeignKey("AttentionTypeId")]
        public AttentionType AttentionType { get; set; }
        [ForeignKey("AttentionCategoryId")]
        public AttentionCategory AttentionCategory { get; set; }

        public ICollection<AttentionDetails> AttentionDetails { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
    }
}

