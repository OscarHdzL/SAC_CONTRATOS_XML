using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_apertura
    {        
        public string id { get; set; }
        public string tbl_solicitud_id { get; set; }
        public string tbl_municipio_id { get; set; }
        public string tbl_tipo_apertura_id { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string direccion { get; set; }
        public string comentario { get; set; }
        public int declaracion_desierta { get; set; }
        public string token { get; set; }

    }
   
}
