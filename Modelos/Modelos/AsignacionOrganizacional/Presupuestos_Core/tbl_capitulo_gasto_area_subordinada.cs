using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_capitulo_gasto_area_subordinada
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public String p_tbl_capitulo_gasto_subarea_id { get; set; }
        public String p_tbl_area_subordinada_id { get; set; }
        public String p_monto_asignado { get; set; }
    }
}
