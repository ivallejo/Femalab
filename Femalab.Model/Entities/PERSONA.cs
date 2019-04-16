using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Model.Entities
{
    public class PERSONA
    {
        public string DNI { get; set; }
        public string APE_PATERNO { get; set; }
        public string APE_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string SEXO { get; set; }
        public string FECHA_NACIMIENTO { get; set; }
        public string UBIGEO_CODIGO { get; set; }
        public string UBIGEO_DIRECCION { get; set; }
    }

}
