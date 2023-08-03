using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class sp_solicitud_en
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public String p_num_solicitud { get; set; }
        public String p_tbl_tipo_solicitud_id { get; set; }
        public String p_tbl_tipo_contrato_id { get; set; }
        public DateTime p_fecha_solicitud { get; set; }
        public String p_elaboro { get; set; }
        public Guid p_tbl_dependencia_id { get; set; }
        public Guid p_tbl_area_id { get; set; }
        public Guid p_tbl_proyecto_id { get; set; }
        public String p_descripcion { get; set; }
        public float p_monto_solicitud { get; set; }
        public float p_monto_autorizado { get; set; }
        public String p_comentarios { get; set; }
        public String p_token_solicitante { get; set; }
        public String p_token_autorizacion { get; set; }
        public String p_tbl_estatus_solicitud_id { get; set; }
        public DateTime p_inclusion { get; set; }
        public String p_json_pres { get; set; }
        public String p_nombre_bien_servicio { get; set; }
        public int p_visitasitio { get; set; }
        public int p_mesa_validacion { get; set; }
    }
}
