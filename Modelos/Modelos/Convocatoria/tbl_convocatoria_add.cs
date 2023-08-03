using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_convocatoria_add
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public Guid p_tbl_solicitud_id { get; set; }
        public Guid p_tbl_servidor_publico_id { get; set; }
        public String p_folio { get; set; }
        public String p_procedimiento { get; set; }
        public DateTime p_fecha_suscripcion { get; set; }
        public DateTime p_inclusion { get; set; }
        public String p_folio_publicacion { get; set; }
        public String p_tipo_publicacion { get; set; }
    }

    public class tbl_obligacion_conv_add
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public Guid p_tbl_convocatoria_id { get; set; }
        public Guid p_tbl_tipo_obligacion_id { get; set; }
        public Guid p_tbl_estatus_obligacion_id { get; set; }
        public String p_obligacion { get; set; }
        public DateTime p_inclusion { get; set; }
    }
}
