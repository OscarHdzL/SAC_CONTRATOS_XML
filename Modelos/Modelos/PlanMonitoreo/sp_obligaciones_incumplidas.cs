using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class sp_obligaciones_incumplidas
    {

        public Guid tbl_obligacion_id { get; set; }
        public String clausula { get; set; }
        public String obligacion { get; set; }
        public String comentarios { get; set; }
        public Guid tbl_plan_cumplimiento_id { get; set; }
        public Guid tbl_link_obligacion_id { get; set; }
        public Guid tbl_plan_entrega_producto_id { get; set; }
        public Guid tbl_tipo_plan_id { get; set; }
       

    }
}
