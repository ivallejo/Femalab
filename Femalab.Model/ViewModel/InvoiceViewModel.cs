using Femalab.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Model.ViewModel
{
    public class InvoiceViewModel
    {
        public Invoice Invoice { get; set; }
        public InvoiceDetails InvoiceDetails { get; set; }
        public Payment Payment { get; set; }
        public Customer Customer { get; set; }
    }
}
