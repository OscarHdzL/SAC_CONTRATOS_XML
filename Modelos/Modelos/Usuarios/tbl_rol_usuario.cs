using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_rol_usuario_request
    {
        public string idUsuario { get; set; }

    }

    public class tbl_rol_usuario_response
    {
        public string id { get; set; }
        public string tbl_rol_id { get; set; }
        public int? principal { get; set; }
        public string nombre { get; set; }
    }

    public class add_rol_usuario_request
    {
        public string idUsuario { get; set; }
        public string idRol { get; set; }
    }

    public class delete_rol_usuario_request
    {
        public string idRolUsuario { get; set; }
    }

    public class update_rol_usuario_request
    {
        public string idRolUsuario { get; set; }
    }
}
