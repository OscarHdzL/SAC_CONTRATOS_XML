using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
        public class tbl_programacion
        {
            public Guid id { get; set; }
            public Guid tbl_tipo_programacion_id { get; set; }
            public DateTime Fecha_Evento { get; set; }
            public String Direccion { get; set; }
            public Guid tbl_ciudad_id { get; set; }
            public String ciudad { get; set; }
            public String Estado { get; set; }
            public Guid tbl_estatus_programacion_id { get; set; }
            public String estatus_programacion { get; set; }
            public String Token { get; set; }
            public Guid tbl_solicitud_id { get; set; }
        }
}
