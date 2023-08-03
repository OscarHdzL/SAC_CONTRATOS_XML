using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_contrato_add
    {
        //public string p_opt { get; set; }
        public string p_id { get; set; }
        public string p_tbl_tipo_contrato_id { get; set; }
        //public int tbl_tipo_contratacion_id { get; set; }
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
        public byte p_ampliacion { get; set; }
        public byte p_requiere_renovacion { get; set; }
        public byte p_satisfactorio { get; set; }
        public string p_porc_max_penalizacion { get; set; }
        public string p_porc_max_deductivas { get; set; }
        public byte p_presento_garantia { get; set; }
        public string p_porc_garantia { get; set; }
        public string p_monto_garantia { get; set; }
        public byte p_es_administradora { get; set; }
        public byte p_activo { get; set; }
        public string p_token { get; set; }
        public string p_nombre { get; set; }
        public string p_monto_max_sin_iva { get; set; }
        public string p_monto_min_sin_iva { get; set; }
        public string p_fecha_registro { get; set; }
        

    }
}
