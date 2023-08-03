using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.EsquemaPago
{
   public class tbl_instancia
    {
        public String id { get; set; }
        public String nombre { get; set; }
        public String nombre_corto { get; set; }
        public bool activo { get; set; }
        public bool es_gobierno_federal { get; set; }
        public String color { get; set; }
        public String token_logo_inicio { get; set; }
        public String hex_col_header { get; set; }
        public String hex_col_sidebar { get; set; }
        public String hex_background { get; set; }
        public String token_logo_mini { get; set; }
        public double iva { get; set; }

    }
}
