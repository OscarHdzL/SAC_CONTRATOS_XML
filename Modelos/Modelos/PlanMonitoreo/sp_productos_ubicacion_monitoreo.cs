using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class sp_productos_ubicacion_monitoreo
    {
        public Guid tbl_plan_entrega_id { get; set; }
        public Guid tbl_plan_monitoreo_id { get; set; }
        public Guid tbl_plan_entrega_producto_id { get; set; }
        public Guid tbl_contrato_producto { get; set; }
        public Guid tbl_producto_servicio_id { get; set; }
        public String producto_servicio { get; set; }
        public String clave_producto { get; set; }
        public String elemento { get; set; }
        public String elemento_desc { get; set; }
        public String comentario { get; set; }
    }

}
