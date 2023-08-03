using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{

    public class contrato_entidad
    {

        public Guid id { get; set; }
        public Guid tbl_tipo_contrato_id { get; set; }
        public Guid tbl_prioridad_id { get; set; }
        public Guid tbl_estatus_contrato_id { get; set; }
        public Guid tbl_proyecto_id { get; set; }
        public Guid tbl_procedimiento_id { get; set; }
        public String numero { get; set; }
        public String objeto { get; set; }
        public DateTime fecha_firma { get; set; }
        public DateTime fecha_Iinicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public DateTime fecha_formalizacion { get; set; }
        public int ampliacion { get; set; }
        public int requiere_renovacion { get; set; }
        public int satisfactorio { get; set; }
        public Double porc_max_penalizacion { get; set; }
        public Double porc_max_deductivas { get; set; }
        public int presento_garantia { get; set; }
        public Double porc_garantia { get; set; }
        public Double monto_garantia { get; set; }
        public int es_administradora { get; set; }
        public int activo { get; set; }
        public String token { get; set; }
        public String nombre { get; set; }
        public Double monto_max_sin_iva { get; set; }
        public Double monto_min_sin_iva { get; set; }
        public DateTime fecha_registro { get; set; }
        public String necesidad { get; set; }


    }
}


namespace Modelos.Modelos.Contrato
{
    public class tbl_contrato
    {
        public string id { get; set; }
        public string tbl_tipo_contrato_id { get; set; }
        //public int tbl_tipo_contratacion_id { get; set; }
        public string tbl_prioridad_id { get; set; }
        public string tbl_estatus_contrato_id { get; set; }
        public string tbl_proyecto_id { get; set; }
        public string tbl_procedimiento_id { get; set; }
        public string numero { get; set; }
        public string objeto { get; set; }
        public DateTime fecha_firma { get; set; }
        public DateTime fecha_Iinicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public DateTime fecha_formalizacion { get; set; }
        public bool ampliacion { get; set; }
        public bool requiere_renovacion { get; set; }
        public bool satisfactorio { get; set; }
        public double porc_max_penalizacion { get; set; }
        public double porc_max_deductivas { get; set; }
        public bool presento_garantia { get; set; }
        public double porc_garantia { get; set; }
        public double monto_garantia { get; set; }
        public bool es_administradora { get; set; }
        public bool activo { get; set; }
        public string token { get; set; }
        public string nombre { get; set; }
        public double monto_max_sin_iva { get; set; }
        public double monto_min_sin_iva { get; set; }
        public DateTime fecha_registro { get; set; }
        //public string tbl_dependencia_id { get; set; }

    }
    
}
