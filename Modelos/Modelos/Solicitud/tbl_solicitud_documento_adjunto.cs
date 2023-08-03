using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_solicitud_documento_adjunto
    {
        public string id { get; set; }
        public string token { get; set; }
        public string tbl_tipo_documento_id { get; set; }
        public string tbl_solicitud_id { get; set; }
        public string nom_documento { get; set; }
    }

    public class tbl_documento_adjunto_solicitud
    {
        public string id { get; set; }
        public string nombre_documento { get; set; }
        public string tipo_documento { get; set; }
        public string token { get; set; }
    }

    public class tbl_solicitud_suficiencia
    {
        public string num_solicitud { get; set; }
        public string folio_solicitud_adq { get; set; }
        public DateTime inclusion { get; set; }
        public string comentarios { get; set; }
        public string tipo_solicitud { get; set; }
        public string tipo_contrato_solicitud { get; set; }
        public DateTime fecha_solicitud { get; set; }
        public string elaboro { get; set; }
        public string dependencia { get; set; }
        public string area { get; set; }
        public string Proyecto { get; set; }
        public string nombre_bien_servicio { get; set; }
        public double monto_autorizado { get; set; }
        public bool visita_sitio { get; set; }
        public string json_pres { get; set; }
        public string solicitante { get; set; }
    }

    public class tbl_suficiencia_add
    {
        public int p_opt { get; set; }
        public string p_id { get; set; }
        public string p_tbl_solicitud_id { get; set; }
        public DateTime p_fecha_autorizacion { get; set; }
        public string p_folio_autorizacion { get; set; }
        public string p_autorizo { get; set; }
        public string p_tbl_fuente_financiamiento_id { get; set; }
        public string p_comentarios { get; set; }
        public string p_sigla { get; set; }
    }

    public class tbl_solicitud_estudio_mercado
    {

        public DateTime fecha_autorizacion_suf { get; set; }
        public string folio_autorizacion_suf { get; set; }
        public string autorizo_suf { get; set; }
        public string fuente_financiamiento_suf { get; set; }
        public string comentarios_suf { get; set; }
        public DateTime fecha_autorizacion_suf_sol { get; set; }

    }

    public class tbl_estudio_mercado
    { 
        public string id { get; set; }
        public string tbl_solicitud_id { get; set; }
        public DateTime fecha_evento_estudio { get; set; }
        public string tbl_usuario_id { get; set; }
        public string sigla_estatus { get; set; }
    }

    public class tbl_tipo_dictamen
    { 
        public string id { get; set; }
        public string tipo_dictamen { get; set; }
        public string descripcion { get; set; }
    }

    public class tbl_dictamen
    { 
        public string id { get; set; }
        public string tbl_solicitud_id { get; set; }
        public DateTime fecha_evento_dictamen { get; set; }
        public string tbl_usuario_id { get; set; }
        public string tbl_tipo_dictamen_id { get; set; }
        public string folio_dictamen { get; set; }
        public string sigla_estatus { get; set; }
    }
}
