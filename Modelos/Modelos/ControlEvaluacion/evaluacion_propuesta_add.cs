using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class evaluacion_propuesta_add
    {
        public int p_opt { get; set; }
        public String p_id { get; set; }
        public String p_tbl_licitante_id { get; set; }
        public String p_analisis { get; set; }       
        public byte p_no_cumplio { get; set; }
        public String p_motivo_incumplimiento { get; set; }
        public byte p_remitir_eval_tec { get; set; }
        public byte p_remitir_eval_eco { get; set; }
        public byte p_ganador { get; set; }

    }
}
