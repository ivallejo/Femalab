using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Model.Custom.Facturalo
{
    public class Facturalo
    {
        public string serie_documento { get; set; }
        public string numero_documento { get; set; }
        public string fecha_de_emision { get; set; }
        public string hora_de_emision { get; set; }
        public string codigo_tipo_operacion { get; set; }
        public string codigo_tipo_documento { get; set; }
        public string codigo_tipo_moneda { get; set; }
        public string fecha_de_vencimiento { get; set; }
        public string numero_orden_de_compra { get; set; }
        public datos_del_emisor datos_del_emisor { get; set; }
        public datos_del_cliente_o_receptor datos_del_cliente_o_receptor { get; set; }
        public totales totales { get; set; }
        public List<items> items { get; set; }

        
    }
    public class datos_del_emisor
    {
        public string codigo_pais { get; set; }
        public string ubigeo { get; set; }
        public string direccion { get; set; }
        public string correo_electronico { get; set; }
        public string telefono { get; set; }
        public string codigo_del_domicilio_fiscal { get; set; }
    }
    public class datos_del_cliente_o_receptor
    {
        public string codigo_tipo_documento_identidad { get; set; }
        public string numero_documento { get; set; }
        public string apellidos_y_nombres_o_razon_social { get; set; }
        public string codigo_pais { get; set; }
        public string ubigeo { get; set; }
        public string direccion { get; set; }
        public string correo_electronico { get; set; }
        public string telefono { get; set; }
    }
    public class totales
    {
        public decimal total_exportacion { get; set; }
        public decimal total_operaciones_gravadas { get; set; }
        public decimal total_operaciones_inafectas { get; set; }
        public decimal total_operaciones_exoneradas { get; set; }
        public decimal total_operaciones_gratuitas { get; set; }
        public decimal total_igv { get; set; }
        public decimal total_impuestos { get; set; }
        public decimal total_valor { get; set; }
        public decimal total_venta { get; set; }
    }
    public class items
    {
        public string codigo_interno { get; set; }
        public string descripcion { get; set; }
        public string codigo_producto_sunat { get; set; }
        public string codigo_producto_gsl { get; set; }
        public string unidad_de_medida { get; set; }
        public decimal cantidad { get; set; }
        public decimal valor_unitario { get; set; }
        public string codigo_tipo_precio { get; set; }
        public decimal precio_unitario { get; set; }
        public string codigo_tipo_afectacion_igv { get; set; }
        public decimal total_base_igv { get; set; }
        public decimal porcentaje_igv { get; set; }
        public decimal total_igv { get; set; }
        public decimal total_impuestos { get; set; }
        public decimal total_valor_item { get; set; }
        public decimal total_item { get; set; }
    }
}

