using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Model.ViewModel
{
    public class ReportVentas
    {
        public Order Order { get; set; }
        public OrderDetails OrderDetails { get; set; }

        //public string Code { get; set; }
    }
    public class Order
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsPay { get; set; }
        public decimal Pay { get; set; }
    }
    public class OrderDetails
    {
        public string Code { get; set; }
        public string Product { get; set; }
        public string Import { get; set; }
    }

}
