using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.Contrato
{
    public class tbl_contrato_vista
    {
        public string id { get; set; }
        public string numero { get; set; }
        public string objeto { get; set; }
        public DateTime fecha_Iinicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public string nombre { get; set; }
        public double monto_max_sin_iva { get; set; }
        public double monto_min_sin_iva { get; set; }
        public string tbl_dependencia_id { get; set; }

    }
}
