using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_plan_monitoreo_lista
    {
        public Guid Id { get; set; }
        public string Identificador { get; set; }
        public string Descripcion { get; set; }
        public String token { get; set; }
        public string PMEstadoDescripcion { get; set; }
        public DateTime? fecha_ejecucion { get; set; }
        public int ContUbicaciones { get; set; }
        public bool Ejecutado { get; set; }

    }
}
