using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApegoContractual.Models
{
    public class Usuario
    {
        public sbyte p_opt { get; set; }
        public string p_id_persona { get; set; }
        public string p_nombre { get; set; }
        public string p_ap_paterno { get; set; }
        public string p_ap_materno { get; set; }
        public string p_email { get; set; }
        public string p_rfc { get; set; }
        public string p_telefono { get; set; }
        public string p_extension { get; set; }
        public string p_tbl_dependencia_id { get; set; }
        public string p_id_usuario { get; set; }
        public string p_usuario { get; set; }
        public string p_password { get; set; }
        public string p_salto { get; set; }
        public sbyte p_activo { get; set; }
        public sbyte p_super_usuario { get; set; }
        public string p_tbl_estatus_autenticacion_id { get; set; }
        public string p_tbl_rol_id { get; set; }
        public string p_tbl_area_id { get; set; }
    }
}
