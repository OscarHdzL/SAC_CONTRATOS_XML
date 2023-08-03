using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
   public class proposiciones
    {
        public Guid tbl_solicitud_id { get; set; }
        public String num_solicitud { get; set; }
        public Guid tbl_convocatoria_id { get; set; }
        public String FolioConvocatoria { get; set; }
        public Guid tbl_licitante_id { get; set; }
        public String licitante { get; set; }
        public String rfc { get; set; }
        public String token_propuesta { get; set; }

    }
}
