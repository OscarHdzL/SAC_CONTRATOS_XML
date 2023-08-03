using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.Usuarios
{
    public class tbl_persona
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public string email { get; set; }
        public string rfc { get; set; }
        public string telefono { get; set; }
        public string extencion { get; set; }
        public string tbl_dependencia_id { get; set; }

    }
}
