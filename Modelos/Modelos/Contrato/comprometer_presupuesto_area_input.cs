using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class comprometer_presupuesto_area_input
    {
        public int p_opt { get; set; }
        public Guid p_tbl_contrato_id { get; set; }
        public Guid p_tbl_dependencia_id { get; set; }
        public Guid p_tbl_capitulo_gasto_id { get; set; }
        public Guid p_tbl_ejercicio_id { get; set; }
        public Guid p_tbl_area_id { get; set; }
        public float p_monto_a_comprometer { get; set; }
    }
    public class PresupuestoContrato
    {
        public String p_capitulo { get; set; }
        public String p_capitulo_des { get; set; }
        public float monto_por_ejercer { get; set; }
        public String des_personal { get; set; }
        public float des_numero { get; set; }
        public Guid idcapgast { get; set; }
        public Guid areaSeleccionada { get; set; }
        public Guid dependencia { get; set; }

    }
}
