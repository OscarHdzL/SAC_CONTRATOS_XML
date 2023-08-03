using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_partida_solicitud_contrato_temp_add
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }        
        public Guid p_tbl_area_id { get; set; }
        public Guid p_tbl_partida_id { get; set; } 
        public string p_tbl_ejercicio_id { get; set; }
        public Guid p_id_propietario { get; set; }
        public string p_numero { get; set; }
        public string p_descripcion { get; set; }
        public string p_monto_ejercido { get; set; }
    }
}
