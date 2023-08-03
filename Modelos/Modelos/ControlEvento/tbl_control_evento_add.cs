using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_control_evento_add
    {
        public int p_opt { get; set; }
        public String p_id { get; set; }
        public String p_tbl_solicitud_id { get; set; }
        public String p_token { get; set; }
        public String p_tbl_tipo_programacion_id { get; set; }
        public String p_estatus { get; set; }
    }
}
