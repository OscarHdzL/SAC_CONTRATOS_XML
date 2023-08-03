using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.EsquemaPago
{
    public class tbl_planes_sin_esquema
    {
        public string tbl_plan_entrega_id { get; set; }
        public string tbl_plan_entrega_identificador { get; set; }
        public string tbl_plan_entrega_descripcion { get; set; }
        public string tbl_contrato_id { get; set; }
        public double? plan_monto { get; set; }
        public double? plan_monto_iva { get; set; }
        public double? plan_total { get; set; }
    }
}
