using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class grid_evaluacion_propuestas_solicitud
    {
        public String tbl_licitante_id { get; set; }
        public String rfc { get; set; }
        public String analisis { get; set; }
        public String motivo_incumplimiento { get; set; }
        public bool? no_cumplio { get; set; }
        public bool? remitir_eval_tec { get; set; }
        public bool? remitir_eval_eco { get; set; }
        public bool? ganador { get; set; }
        public String token_tec { get; set; }
        public String token_eco { get; set; }

    }
}
