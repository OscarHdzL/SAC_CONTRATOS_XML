using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.VerificacionContrato
{
    public class tbl_verificacion_contrato
    {
        public String id { get; set; }
        public String tbl_contrato_id { get; set; }
        public String tbl_usuario_id { get; set; }
        public String tbl_pregunta_formulario_id { get; set; }
        public String tbl_estatus_verificacion_id { get; set; }
        public DateTime inclusion { get; set; }
        public DateTime? fecha_verificacion { get; set; }
        public String pregunta_personalizada { get; set; }
    }
}
