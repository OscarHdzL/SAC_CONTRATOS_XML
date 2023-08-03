using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_acuerdo_add
    {
            public int p_opt { get; set; }
            public string p_id { get; set; }
            public string p_tbl_contrato_id { get; set; }
            public string p_tbl_contrato_servidor_resp_id { get; set; }
            public string p_tbl_tipo_acuerdo_id { get; set; }
            public string p_acuerdo { get; set; }
            public string p_fecha_registro { get; set; }
            public string p_fecha_compromiso { get; set; }
            public string p_fecha_cierre { get; set; }
            public string p_estatus_acuerdo { get; set; }
            public string p_comentario { get; set; }
            //public string p_inclusion { get; set; }
            public byte p_estatus { get; set; }

    }
}
