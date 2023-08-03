using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_tipo_ejecucion
    {
        public Guid id { get; set; }
        public String tipo_ejecucion { get; set; }
        public Guid tbl_instancia_id { get; set; }
        public int activo { get; set; }

    }
    public class tbl_tipo_ejecucion_add 
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public String p_tipo_ejecucion { get; set; }
        public Guid p_tbl_instancia_id { get; set; }
    }
}
