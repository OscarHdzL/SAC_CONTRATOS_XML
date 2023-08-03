using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
   public class proposiciones_evaluadas
    {
        public Guid tbl_proposicion_tecnica_economica_id { get; set; }
        public Guid tbl_licitante_id { get; set; }
        public Guid tbl_solicitud_id { get; set; }
        public String analisis { get; set; }
        public String justificacion { get; set; }
        public Boolean cumplimiento { get; set; }
        public String licitante { get; set; }
        public String rfc { get; set; }
        public String num_solicitud { get; set; }

    }
}
