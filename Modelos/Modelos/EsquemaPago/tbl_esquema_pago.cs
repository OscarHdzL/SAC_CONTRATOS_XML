using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.EsquemaPago
{
    public class tbl_esquema_pago_new
    {
        public string id { get; set; }
        public string tbl_contrato_servidor_resp_id { get; set; }
        public string nombre_responsable { get; set; }
        public string tbl_contrato_proveedor_id { get; set; }
        public string razon_social { get; set; }
        public DateTime fecha_pago { get; set; }
        public double monto { get; set; }
        public double monto_iva { get; set; }
        public double total { get; set; }
        public string estado_plan_entrega { get; set; }
        public bool tiene_firma { get; set; }
        public string observacion { get; set; }
        public string token_fac { get; set; }
        public DateTime inclusion { get; set; }
        public bool estatus { get; set; }
        public string notificacion_tbl_proveedor_id { get; set; }
        public string tbl_plan_entrega_id { get; set; }
    }

    public class tbl_esquema_pago_info_correo
    {
        public string id { get; set; }
        public string tbl_contrato_servidor_resp_id { get; set; }
        public string nombre_responsable { get; set; }
        public string tbl_contrato_proveedor_id { get; set; }
        public string razon_social { get; set; }
        public DateTime fecha_pago { get; set; }
        public double monto { get; set; }
        public double monto_iva { get; set; }
        public double total { get; set; }
        public string estado_plan_entrega { get; set; }
        public bool tiene_firma { get; set; }
        public string observacion { get; set; }
        public string token_fac { get; set; }
        public DateTime inclusion { get; set; }
        public bool estatus { get; set; }
        public string notificacion_tbl_proveedor_id { get; set; }
        public string notificacion_correo { get; set; }
    }
}
