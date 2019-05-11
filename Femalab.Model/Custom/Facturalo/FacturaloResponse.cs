using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Model.Custom.Facturalo
{
    public class FacturaloResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string file { get; set; }
        public string line { get; set; }
        public data data { get; set; }
        public links links { get; set; }
        public object response { get; set; }
    }
    public class data
    {
        public string number { get; set; }
        public string filename { get; set; }
        public string external_id { get; set; }
        public string number_to_letter { get; set; }
        public string hash { get; set; }
        public string qr { get; set; }

    }
    public class links
    {
        public string xml { get; set; }
        public string pdf { get; set; }
        public string cdr { get; set; }
    }

}
