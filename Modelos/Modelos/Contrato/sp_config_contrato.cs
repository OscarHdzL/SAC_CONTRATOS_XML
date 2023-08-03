
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class sp_config_contrato
    {
        public String p_opt { get; set; }
        public Guid p_id { get; set; }
        public Guid p_tbl_tipo_contrato_id { get; set; }
        public Guid p_tbl_prioridad_id { get; set; }
        public Guid p_tbl_estatus_contrato_id { get; set; }
        public Guid p_tbl_proyecto_id { get; set; }
        public Guid p_tbl_procedimiento_id { get; set; }
        public String p_numero { get; set; }
        public String p_objeto { get; set; }
        public DateTime p_fecha_firma { get; set; }
        public DateTime p_fecha_Iinicio { get; set; }
        public DateTime p_fecha_fin { get; set; }
        public DateTime p_fecha_formalizacion { get; set; }
        public int p_ampliacion { get; set; }
        public int p_requiere_renovacion { get; set; }
        public int p_satisfactorio { get; set; }
        public Double p_porc_max_penalizacion { get; set; }
        public Double p_porc_max_deductivas { get; set; }
        public int p_presento_garantia { get; set; }
        public Double p_porc_garantia { get; set; }
        public Double p_monto_garantia { get; set; }
        public int p_es_administradora { get; set; }
        public int p_activo { get; set; }
        public String p_token { get; set; }
        public String p_nombre { get; set; }
        public Double p_monto_max_sin_iva { get; set; }
        public Double p_monto_min_sin_iva { get; set; }
        public DateTime p_fecha_registro { get; set; }
        public String p_necesidad { get; set; }
        public Guid p_tbl_dependencia_id { get; set; }
        public Guid p_estructura_asignado_id { get; set; }
        public int p_tipo_estructura { get; set; }
    }
    public class sp_config_contrato_
    {
        public String p_opt { get; set; }
        public Guid p_id { get; set; }
        public Guid p_tbl_tipo_contrato_id { get; set; }
        public Guid p_tbl_prioridad_id { get; set; }
        public Guid p_tbl_estatus_contrato_id { get; set; }
        public Guid p_tbl_proyecto_id { get; set; }
        public Guid p_tbl_procedimiento_id { get; set; }
        public String p_numero { get; set; }
        public String p_objeto { get; set; }
        public DateTime p_fecha_firma { get; set; }
        public DateTime p_fecha_Iinicio { get; set; }
        public DateTime p_fecha_fin { get; set; }
        public DateTime p_fecha_formalizacion { get; set; }
        public Boolean p_ampliacion { get; set; }
        public Boolean p_requiere_renovacion { get; set; }
        public Boolean p_satisfactorio { get; set; }
        public Double p_porc_max_penalizacion { get; set; }
        public Double p_porc_max_deductivas { get; set; }
        public Boolean p_presento_garantia { get; set; }
        public Double p_porc_garantia { get; set; }
        public Double p_monto_garantia { get; set; }
        public Boolean p_es_administradora { get; set; }
        public Boolean p_activo { get; set; }
        public String p_token { get; set; }
        public String p_nombre { get; set; }
        public Double p_monto_max_sin_iva { get; set; }
        public Double p_monto_min_sin_iva { get; set; }
        public DateTime p_fecha_registro { get; set; }
        public String p_necesidad { get; set; }
        public Guid? p_tbl_dependencia_id { get; set; }
        public Guid? p_estructura_asignado_id { get; set; }
        public int? p_tipo_estructura { get; set; }

    }
    
}
