using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
        public class ProgramacionEventosEntidad
        {
            public Guid p_id { get; set; }
         
            public Guid p_tbl_tipo_programacion_id { get; set; }
            public DateTime p_Fecha_Evento { get; set; }
            public String p_direccion { get; set; }
            public Guid p_tbl_ciudad_id { get; set; }
            public Guid p_tbl_estatus_programacion_id { get; set; }
            public DateTime p_inclusion { get; set; }
            public String p_token { get; set; }
            public int p_action { get; set; }
            public Guid p_tbl_solicitud_id { get; set; }
    }

}
