using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_producto_servicio_contrato
    {
        public Guid id { get; set; }
        public Guid tbl_contrato_producto_id { get; set; }
        public Guid tbl_dependencia_id { get; set; }
        public String producto_servicio { get; set; }
        public String clave_producto { get; set; }
        public int cantidad_minima { get; set; }
        public int cantidad_maxima { get; set; }
        public decimal unitario { get; set; }
        public String elemento { get; set; }
        public String elemento_desc { get; set; }
        public Guid tbl_unidad_medida_id { get; set; }
        public String unidad_medida { get; set; }
        public DateTime inclusion { get; set; }
        public Boolean? activo { get; set; }
        public String comentario { get; set; }
        public Guid tbl_tipo_id { get; set; }
        public String tipo { get; set; }
    }
}
