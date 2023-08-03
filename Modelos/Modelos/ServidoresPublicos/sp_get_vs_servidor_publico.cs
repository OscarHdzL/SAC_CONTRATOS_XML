using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.ServidoresPublicos
{
    public class sp_get_vs_servidor_publico_input
    {
        public String id { get; set; }
        public String tipo { get; set; }
        public Boolean Escontrato { get; set; }
    }

    public class sp_get_vs_servidor_publico_contrato_c
    {
        public Guid tbl_contrato_servidor_resp_id { get; set; }
        public Guid tbl_contrato_id { get; set; }
        public String nuemrocontrato { get; set; }
        public Guid tbl_rol_usuario_id { get; set; }
        public Guid tbl_persona_id { get; set; }
        public String nombrecompleto { get; set; }
        public String nombre { get; set; }
        public String ap_paterno { get; set; }
        public String ap_materno { get; set; }
        public String email { get; set; }
        public String rfc { get; set; }
        public String telefono { get; set; }
        public String extencion { get; set; }
        public Guid tbl_dependencia_id { get; set; }
        public String dependencia { get; set; }
        public String tbl_area_id { get; set; }
        public String area { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public String rol { get; set; }
        public String sigla { get; set; }
        public Boolean estatus_tbl_contrato_servidor_resp { get; set; }
    }





    public class sp_get_vs_servidor_publico_contrato
    {
        public Guid tbl_contrato_id { get; set; }
        public String nuemrocontrato { get; set; }
        public Guid tbl_rol_usuario_id { get; set; }
        public Guid tbl_persona_id { get; set; }
        public String nombrecompleto { get; set; }
        public String nombre { get; set; }
        public String ap_paterno { get; set; }
        public String ap_materno { get; set; }
        public String email { get; set; }
        public String rfc { get; set; }
        public String telefono { get; set; }
        public String extencion { get; set; }
        public Guid tbl_dependencia_id { get; set; }
        public String dependencia { get; set; }
        public String tbl_area_id { get; set; }
        public String area { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public String rol { get; set; }
        public String sigla { get; set; }
        public Guid tbl_contrato_servidor_resp_id { get; set; }
    }



    public class sp_get_vs_servidor_publico_
    {
        public Guid tbl_contrato_id { get; set; }
        public String nuemrocontrato { get; set; }
        public Guid tbl_rol_usuario_id { get; set; }
        public Guid tbl_persona_id { get; set; }
        public String nombrecompleto { get; set; }
        public String nombre { get; set; }
        public String ap_paterno { get; set; }
        public String ap_materno { get; set; }
        public String email { get; set; }
        public String rfc { get; set; }
        public String telefono { get; set; }
        public String extencion { get; set; }
        public Guid tbl_dependencia_id { get; set; }
        public String dependencia { get; set; }
        public String tbl_area_id { get; set; }
        public String area { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public String rol { get; set; }
        public String sigla { get; set; }
        
    }


}
