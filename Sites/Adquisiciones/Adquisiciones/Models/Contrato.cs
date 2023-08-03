using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeAdquisiciones.Models
{
    public class Contrato
    {
        //public string IdContratoTemp { get; set; }
        //public string IdProyecto { get; set; }
        //public string IdPrioridad { get; set; }
        //public string IdProcedimiento { get; set; }
        //public string IdEstatusContrato { get; set; }
        //public string Id_TipoContrato { get; set; }
        //public string Numero { get; set; }
        //public string Objeto { get; set; }
        //public string FechaFirma { get; set; }
        //public string FechaInicio { get; set; }
        //public string FechaFin { get; set; }
        //public string FechaFormalizacion { get; set; }
        //public string Ampliacion { get; set; }
        //public string RequiereRenovacion { get; set; }
        //public string Satisfactorio { get; set; }
        //public string PresentoGarantia { get; set; }
        //public string EsAdministradora { get; set; }
        //public string IdAlertamiento { get; set; }

        public string p_id { get; set; }
        public string p_tbl_tipo_contrato_id { get; set; }
        public string p_tbl_prioridad_id { get; set; }
        public string p_tbl_estatus_contrato_id { get; set; }
        public string p_tbl_proyecto_id { get; set; }
        public string p_tbl_procedimiento_id { get; set; }
        public string p_numero { get; set; }
        public string p_objeto { get; set; }
        public string p_fecha_firma { get; set; }
        public string p_fecha_Iinicio { get; set; }
        public string p_fecha_fin { get; set; }
        public string p_fecha_formalizacion { get; set; }
        public sbyte p_ampliacion { get; set; }
        public sbyte p_requiere_renovacion { get; set; }
        public sbyte p_satisfactorio { get; set; }
        public string p_porc_max_penalizacion {get; set;}
        public string p_porc_max_deductivas {get; set;}
        public sbyte p_presento_garantia { get; set; }
        public string p_porc_garantia {get; set;}
        public string p_monto_garantia { get; set; }
        public sbyte p_es_administradora { get; set; }
        public sbyte p_activo { get; set; }
        public string p_token { get; set; }
        public string p_nombre { get; set; }
        public string p_monto_max_sin_iva { get; set; }
        public string p_monto_min_sin_iva { get; set; }
        public string p_fecha_registro { get; set; }
    }
}
