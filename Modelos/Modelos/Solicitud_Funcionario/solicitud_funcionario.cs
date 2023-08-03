using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
   public class solicitud_funcionario
    {
        public Guid id { get; set; }
        public Guid tbl_solicitud_id { get; set; }
        public Guid tbl_convocatoria_id { get; set; }
        public String FolioConvocatoria { get; set; }
        public Guid tbl_servidor_publico_id { get; set; }
        public String servidor_publico { get; set; }
        public String tipo_acta { get; set; }
        public Guid tbl_programacion_id { get; set; }
        public DateTime inclusion { get; set; }
        public bool activo { get; set; }
        public String num_solicitud { get; set; }

    }
}
