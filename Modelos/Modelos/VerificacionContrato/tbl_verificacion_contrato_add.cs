using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.VerificacionContrato
{
    public class tbl_verificacion_contrato_add
    {
        public int p_opt { get; set; }
        public String p_id { get; set; }
        public String p_tbl_contrato_id { get; set; }
        public String p_tbl_usuario_id { get; set; }
        public String p_tbl_pregunta_formulario_id { get; set; }
        public String p_tbl_estatus_verificacion_id { get; set; }
        public String p_inclusion { get; set; }
        public DateTime? p_fecha_verificacion { get; set; }
        public String p_pregunta_personalizada { get; set; }

    }
}
