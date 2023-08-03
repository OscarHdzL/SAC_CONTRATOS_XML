using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
   public class solicitud_funcionario_add
    {
        public int p_opt { get; set; }
        public String p_id { get; set; }
        //public String p_tbl_solicitud_id { get; set; }
        public String p_tbl_servidor_publico_id { get; set; }
        public String p_tipo_acta { get; set; }
        public String p_tbl_programacion_id { get; set; }
        
    }
}
