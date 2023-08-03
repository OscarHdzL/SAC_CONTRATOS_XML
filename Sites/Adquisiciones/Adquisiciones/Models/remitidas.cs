using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeAdquisiciones.Models
{
   public class remitidas
    {
        public Guid tbl_convocatoria_id { get; set; }
        public String FolioConvocatoria { get; set; }
        public Guid tbl_solicitud_id { get; set; }
        public String num_solicitud { get; set; }
        public DateTime fecha_solicitud { get; set; }
    }
}
