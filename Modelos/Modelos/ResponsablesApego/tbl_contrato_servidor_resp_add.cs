using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.ResponsablesApego
{
    public class tbl_contrato_servidor_resp_add
    {
        public int p_opt { get; set; }
        public string p_id { get; set; }
        //public string p_tbl_responsabilidad_id { get; set; }
        public string p_tbl_contrato_id { get; set; }
        public string p_inclusion { get; set; }
        public byte p_estatus { get; set; }
        public string p_tbl_rol_usuario_id { get; set; }
    }
}
