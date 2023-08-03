using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.Dependencia
{
    public class tbl_instancia_contrato
    {
        public int opt { get; set; }
        public string id { get; set; }
        public string nombre { get; set; }
        public string nombre_corto { get; set; }
        public string copyright { get; set; }
        public int gob_fed { get; set; }
        public string token_logo_mini { get; set; }
        public string token_logo_inicio { get; set; }
        public string hex_col_header { get; set; }
        public string hex_col_sidebar { get; set; }
        public string hex_background { get; set; }
        public string hex_textcolor { get; set; }

    }

    public class tbl_instancia_contrato_get
    {
        public string copyright { get; set; }
        public string hex_col_header { get; set; }
        public string hex_col_sidebar { get; set; }
        public string hex_background { get; set; }
        public string token_logo_mini { get; set; }
        public string token_logo_inicio { get; set; }
        public string hex_textcolor { get; set; }

    }

}
