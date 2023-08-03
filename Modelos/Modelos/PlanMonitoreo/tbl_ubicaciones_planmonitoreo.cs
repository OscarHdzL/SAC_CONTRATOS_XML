using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_ubicaciones_planmonitoreo
    {
        public Guid tbl_plan_monitoreo_ubicacion_id { get; set; }
        public Guid tbl_plan_monitoreo_id { get; set; }
        public Guid tbl_ubicacion_plan_entrega_id { get; set; }
        public Guid tbl_ubicacion_id { get; set; }
        public string clave { get; set; }
        public string unidad { get; set; }
        public string direccion { get; set; }

    }
}
