using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class sp_obligaciones_ubicacion_producto_planmonitoreo
    {
        public Guid tbl_plan_entrega_id { get; set; }
        public Guid tbl_plan_monitoreo_id { get; set; }
        public Guid tbl_contrato_producto_id { get; set; }
        public Guid tbl_producto_servicio_id { get; set; }
        public Guid tbl_plan_entrega_producto_id { get; set; }
        public string elemento { get; set; }
        public Guid tbl_obligacion_id { get; set; }
        public Guid tbl_link_obligacion_id { get; set; }
        public string clausula { get; set; }
        public string obligacion { get; set; }
        public String token_obligacion { get; set; }
        public Guid tbl_tipo_plan_id { get; set; }

        

    }
}
