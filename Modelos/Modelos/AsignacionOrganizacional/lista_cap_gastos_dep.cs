using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class lista_cap_gastos_dep
    {
        public Guid id { get; set; }
        public string numero { get; set; }
        public string descripcion { get; set; }
        public Double monto_asignado { get; set; }
        public Guid tbl_partida_dependencia_id { get; set; }
    }
}
