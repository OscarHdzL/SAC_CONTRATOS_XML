using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_contrato_area_add
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public Guid p_tbl_contrato_id { get; set; }
        public Guid p_tbl_area_id { get; set; }
        public Guid p_tbl_partida_id { get; set; }
        public string p_tbl_ejercicio_id { get; set; }
        public string p_monto_ejercido { get; set; }
    }
}
