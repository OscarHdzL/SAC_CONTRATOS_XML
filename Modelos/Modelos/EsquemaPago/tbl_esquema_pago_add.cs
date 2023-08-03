using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.EsquemaPago
{
    public class tbl_esquema_pago_add
    {
        public int p_opt { get; set; }
        public string p_id { get; set; }
        public string p_tbl_contrato_servidor_resp_id { get; set; }
        public string p_tbl_contrato_proveedor_id { get; set; }
        public string p_fecha_pago { get; set; }
        public string p_monto { get; set; }
        public string p_monto_iva { get; set; }
        public string p_total { get; set; }
        public string p_estado_plan_entrega { get; set; }
        public byte p_tiene_firma { get; set; }
        public string p_observacion { get; set; }
        public string p_token_fac { get; set; }
        public string p_inclusion { get; set; }
        public byte p_estatus { get; set; }
        public string p_tbl_plan_entrega_id { get; set; }
        public string p_notificacion_proveedor_id { get; set; }
    }
}
