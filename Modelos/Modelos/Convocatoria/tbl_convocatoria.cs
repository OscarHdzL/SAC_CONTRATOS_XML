using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_convocatoria
    {
        public Guid id { get; set; }
        public Guid tbl_solicitud_id { get; set; }
        public String num_solicitud { get; set; }
        public String area { get; set; }
        public String descripcion { get; set; }
        public string tbl_servidor_publico_id { get; set; }
        public String nombre_servidor_publico { get; set; }
        public String folio { get; set; }
        public String procedimiento { get; set; }
        public DateTime fecha_suscripcion { get; set; }
        public DateTime inclusion { get; set; }
        public String folio_publicacion { get; set; }
        public String tipo_publicacion { get; set; }
        public String tipo_licitacion { get; set; }
    }
    public class tbl_convocatoria_obligaciones
    {
        public Guid id { get; set; }
        public Guid tbl_convocatoria_id { get; set; }
        public Guid tbl_tipo_obligacion_id { get; set; }
        public Guid tbl_estatus_obligacion_id { get; set; }
        public String obligacion { get; set; }
        public DateTime inclusion { get; set; }
        public bool activo { get; set; }
        public string folio { get; set; }
        public string tipo_obligacion { get; set; }
    }

}
