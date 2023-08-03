using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_usuario_verifica
    {

        public string id { get; set; }
        public string usuario { get; set; }
        public string nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public string password { get; set; }
        public string salto { get; set; }
        public bool? activo { get; set; }
        public bool? super_usuario { get; set; }
        public string tbl_estatus_autenticacion_id { get; set; }
        public string estatus_autenticacion { get; set; }
        public string tbl_dependencia_id { get; set; }
        public string tbl_rol_id{ get; set; }
        public string tbl_instancia_id { get; set; }
        public string tbl_rol_usuario_id { get; set; }

    }
}
