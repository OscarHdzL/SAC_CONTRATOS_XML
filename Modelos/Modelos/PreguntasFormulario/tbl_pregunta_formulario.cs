using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.PreguntasFormulario
{
    public class tbl_pregunta_formulario
    {
        public String id { get; set; }
        public String tbl_dependencia_id { get; set; }
        public String pregunta { get; set; }
        public DateTime inclusion { get; set; }
        public bool estatus { get; set; }
    }
}
