using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{ 
    public class comentario_add
    {
        public int p_opt { get; set; }
        public String p_id { get; set; }
        public String p_tbl_solicitud_id { get; set; }
        public String p_tbl_usuario_id { get; set; }
        public String p_sigla_fase { get; set; }
        public String p_comentario { get; set; }
    }
}
