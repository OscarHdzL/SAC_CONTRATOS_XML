using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_solicitud
    {

        public Guid id { get; set; }
        public String num_solicitud { get; set; }
        public String tbl_tipo_solicitud_id { get; set; }
        public String tipo_solicitud { get; set; }
        public String tbl_tipo_contrato_id { get; set; }
        public String tipo_contrato { get; set; }
        public DateTime fecha_solicitud { get; set; }
        public String elaboro { get; set; }
        public Guid tbl_dependencia_id { get; set; }
        public String dependencia { get; set; }
        public Guid tbl_area_id { get; set; }
        public String area { get; set; }
        public Guid tbl_proyecto_id { get; set; }
        public String proyecto { get; set; }
        public String descripcion { get; set; }
        public float monto_solicitud { get; set; }
        public float monto_autorizado { get; set; }
        public String comentarios { get; set; }
        public String token_solicitante { get; set; }
        public String token_autorizacion { get; set; }
        public Guid tbl_estatus_solicitud_id { get; set; }
        public String estatus_solicitud { get; set; }
        public String sigla_estatus_solicitud { get; set; }
        public DateTime inclusion { get; set; }
        public String token_modalidad { get; set; }
        public String tipo_licitacion { get; set; }
        public String nombre_bien_servicio { get; set; }
        public bool visita_sitio { get; set; }
        public bool mesa_validacion { get; set; }
        public string json_pres { get; set; }
        public string comentario_suficiencia { get; set; }
        public bool turnada_sdrm { get; set; }
        public bool turnar_integr_precios { get; set; }
        public bool docum_completa { get; set; }
        public bool requiere_dictamen { get; set; }
    }
}
