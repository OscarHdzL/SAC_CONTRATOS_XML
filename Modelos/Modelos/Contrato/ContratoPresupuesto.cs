using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class ContratoPresupuesto
    {
        public Guid id { get; set; }
        public Guid tbl_area_id { get; set; }
        public String area { get; set; }
        public Guid tbl_capitulo_gasto_id { get; set; }
        public String codigo_capitulo { get; set; }
        public String capitulo { get; set; }
        public decimal monto_asignado { get; set; }
        public decimal monto_disponible { get; set; }
    }
}
