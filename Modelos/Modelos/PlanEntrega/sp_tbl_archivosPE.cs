using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class sp_tbl_archivosPE
    {
        public Guid id_ { get; set; }
        public Guid tbl_plan_entrega_id_ { get; set; }
        public Guid tbl_Ubicacion_id_ { get; set; }
        public Guid tbl_producto_servicio_id_ { get; set; }
        public String token_ { set; get; }
    }
}
