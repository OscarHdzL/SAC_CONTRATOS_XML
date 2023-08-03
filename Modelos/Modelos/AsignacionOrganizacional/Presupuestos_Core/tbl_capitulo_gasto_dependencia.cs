using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_capitulo_gasto_dependencia
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public String p_tbl_dependencia_id { get; set; }
        public String p_tbl_capitulo_gasto_id { get; set; }
        public String p_tbl_ejercicio_id { get; set; }
        public String p_monto_asignado { get; set; }
    }
}
