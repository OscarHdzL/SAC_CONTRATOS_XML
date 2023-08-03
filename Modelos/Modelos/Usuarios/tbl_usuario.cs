using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_usuario
    {
        public string id { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string salto { get; set; }
        public bool? activo { get; set; }
        public bool? super_usuario { get; set; }
        public string tbl_estatus_autenticacion_id { get; set; }
        public string tbl_persona_id { get; set; }
        public string tbl_instancia_id { get; set; }
        public string email { get; set; }

    }
    public class dependencias_usuario 
    {
        public string usuario_id { get; set; }
        public string tbl_dependencia_id { get; set; }
        public int principal { get; set; }
        public string nombre_dependencia { get; set; }
    }

}
