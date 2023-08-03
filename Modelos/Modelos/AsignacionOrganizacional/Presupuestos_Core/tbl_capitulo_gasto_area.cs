using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_capitulo_gasto_area
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public String p_tbl_capitulo_gasto_dependencia_id { get; set; }
        public String p_tbl_area_id { get; set; }
        public string p_monto_asignado { get; set; }
    }
}
