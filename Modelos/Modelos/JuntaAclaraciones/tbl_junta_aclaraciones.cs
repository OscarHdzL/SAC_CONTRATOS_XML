using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_junta_aclaraciones
    {
        public string id { get; set; }
        public string tbl_solicitud_id { get; set; }
        public int numero_junta { get; set; }
        public string aclaracion { get; set; }
        public DateTime fecha_aclaracion { get; set; }
        public int req_junta { get; set; }
        
    }

    public class tbl_junta_aclaraciones_list
    {        
        public string id { get; set; }
        public DateTime fecha_aclaracion { get; set; }
        public string num_solicitud { get; set; }
        public int numero_junta { get; set; }
        public string aclaracion { get; set; }

    }
}
