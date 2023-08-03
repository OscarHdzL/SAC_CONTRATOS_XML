using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.ServidoresPublicos
{
    public class tbl_servidor_publico
    {
        public string id { get; set; }
        //public string tbl_puesto_id { get; set; }
        public string nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public string rfc { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string extencion { get; set; }
        public string tbl_rol_id { get; set; }
        public string rol { get; set; }
        public string tbl_area_id { get; set; }

    }
}
