using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class lista_cap_gastos_areas
    {
        public Guid id { get; set; }
        public string numero { get; set; }
        public string descripcion { get; set; }
        public Double monto_asignado_dep { get; set; }
        public Double monto_disponible { get; set; }
        public Double monto_asignado_area { get; set; }
    }
}
