using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.PreguntasFormulario
{
    public class tbl_pregunta_formulario_add
    {
        public int p_opt { get; set; }
        public String p_id { get; set; }
        public String p_tbl_dependencia_id { get; set; }
        public String p_pregunta { get; set; }
        public String p_inclusion { get; set; }
        public byte p_estatus { get; set; }
    }
}
