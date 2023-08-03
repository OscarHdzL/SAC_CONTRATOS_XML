using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_usuarios
    {

        public string id_usuario { get; set; }
        public string id_persona { get; set; }
        public string email { get; set; }
        public string nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public bool? activo { get; set; }
        public string rfc { get; set; }
        public string telefono { get; set; }
        public string extencion { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public string id_dependencia { get; set; }
        public string id_area { get; set; }
        public string id_rol { get; set; }
        public int super_usuario { get; set; }
        public string usuario { get; set; }
        public List<tbl_usuarios_dependencias_extra> dependencias_adicionales { get; set; }
    }
    public class tbl_usuarios_dependencias_extra 
    { 
        public string tbl_dependencia_id { get; set; }
    }
    public class tbl_usuarios_list
    {

        public string id_usuario { get; set; }
        public string id_persona { get; set; }
        public string email { get; set; }
        public string nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public bool? activo { get; set; }
        public string rfc { get; set; }
        public string telefono { get; set; }
        public string extencion { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public string id_dependencia { get; set; }
        public string id_area { get; set; }
        public string id_rol { get; set; }
        public bool super_usuario { get; set; }
        public string usuario { get; set; }

    }
}
