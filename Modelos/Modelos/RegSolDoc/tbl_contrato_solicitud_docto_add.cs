using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.RegSolDoc
{
   public class tbl_contrato_solicitud_docto_add
    {
        public int p_opt { get; set; }
        public string p_id { get; set; }
        //public string p_tbl_contrato_id { get; set; }
        public string p_nombre_documento { get; set; }       
        public string p_tbl_contrato_servidor_resp_id { get; set; }
        public string p_fecha_solicitud { get; set; }
        public string p_fecha_entrega { get; set; }
        public string p_tipo_entregable { get; set; }
        public string p_observacion { get; set; }
        public string p_correo_solicitud { get; set; }
        public string p_inclusion { get; set; }
        public byte p_estatus { get; set; }

    }
}
