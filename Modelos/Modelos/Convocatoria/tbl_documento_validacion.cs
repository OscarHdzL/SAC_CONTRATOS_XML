using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_documento_validacion
    {
        public int p_opt { get; set; }                
	    public Guid p_id { get; set; }
        public Guid p_tbl_convocatoria_id { get; set; }
        public Guid p_tbl_tipo_documento_id { get; set; }
        public String p_justificacion { get; set; }
    }
    public class tbl_documento_validacion_lista
    {
        public Guid id { get; set; }
        public Guid tbl_convocatoria_id { get; set; }
        public Guid tbl_tipo_documento_id { get; set; }
        public String justificacion { get; set; }
        public String folio { get; set; }
        public String tipo_documento { get; set; }
    }
}
