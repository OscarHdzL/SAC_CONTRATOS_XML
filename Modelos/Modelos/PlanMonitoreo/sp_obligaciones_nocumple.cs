using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class sp_obligaciones_nocumple
    {
        public Guid tbl_plan_cumplimiento_id { get; set; }
        public Guid tbl_link_obligacion_id { get; set; }
        public Guid tbl_plan_entrega_producto_id { get; set; }
        public Guid tbl_tipo_plan_id { get; set; }
       

    }
}
