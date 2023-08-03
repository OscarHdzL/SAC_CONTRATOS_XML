using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_tipo_prioridad
    {
        public Guid id { get; set; }
        public String tipo_prioridad { get; set; }
        public Guid tbl_instancia_id { get; set; }
        public int activo { get; set; }

    }
    public class tbl_tipo_prioridad_add
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public String p_tipo_prioridad { get; set; }
        public Guid p_tbl_instancia_id { get; set; }
    }
}
