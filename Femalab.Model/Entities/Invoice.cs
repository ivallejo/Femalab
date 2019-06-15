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
    [Table("Invoice")]
    public class Invoice : AuditableEntity<long>
    {
        [MaxLength(2)]
        public string VoucherType { get; set; }
        [MaxLength(2)]
        public string Series { get; set; }
        public long DocumentNumber { get; set; }
        [MaxLength(3)]
        public string Currency { get; set; }
        public decimal IGV { get; set; }
        public decimal TotalValue { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TotalTax { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }        
        [MaxLength(250)]
        public string Observations { get; set; }

        public bool ApiSuccess { get; set; }
        public string ApiMessage { get; set; }
        public string ApiFile { get; set; }
        public string ApiLine { get; set; }

        public string SunatNumber { get; set; }
        public string SunatFilename { get; set; }
        public string SunatExternalId { get; set; }
        public string SunatNumberToLetter { get; set; }
        public string SunatHash { get; set; }
        public string SunatQr { get; set; }
        public string SunatPdf { get; set; }
        public string SunatXml { get; set; }
        public string SunatCdr { get; set; }
        public long SunatState { get; set; }

        public long CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public long AttentionId { get; set; }
        [ForeignKey("AttentionId")]
        public Attention Attention { get; set; }

        public ICollection<InvoiceDetails> InvoiceDetails { get; set; }
        public ICollection<Payment> Payments { get; set; }

    }
}
