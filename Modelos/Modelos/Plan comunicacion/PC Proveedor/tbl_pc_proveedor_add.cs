using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_pc_proveedor_add
    {
        public int p_opt { get; set; }
        public string p_id { get; set; }
        public string p_tbl_proveedor_id { get; set; }
        public string p_tbl_contrato_id { get; set; }
        public string p_tbl_tipo_audiencia_id { get; set; }
        public string p_inclusion { get; set; }
        public byte p_activo { get; set; }
    }
}
