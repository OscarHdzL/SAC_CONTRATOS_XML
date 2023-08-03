using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class info_interlocutor_comercial
    {
        public String id { get; set; } 
        public String tbl_dependencia_id { get; set; }
        public String razon_social { get; set; }
        public String rfc { get; set; }
        public String tbl_tipo_interlocutor_id { get; set; }
        public String nombre { get; set; }
        public bool? comercial { get; set; }
        public bool? activo { get; set; }
    }
    public class proveedor_dependencia 
    { 
        public String tbl_dependencia_id { get; set; }
        public String tbl_proveedor_id { get; set; }
        public String nombre_dependencia { get; set; }
    }

}
