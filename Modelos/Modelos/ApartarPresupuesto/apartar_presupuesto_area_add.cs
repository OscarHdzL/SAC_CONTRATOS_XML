using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
   public class apartar_presupuesto_area_add
    {
        public int p_opt_id { get; set; }
        public String p_tbl_origen_recurso_id { get; set; }
        public String p_tbl_capitulo_gastos_area { get; set; }
        public Double p_monto_apartar { get; set; }
    }
}
