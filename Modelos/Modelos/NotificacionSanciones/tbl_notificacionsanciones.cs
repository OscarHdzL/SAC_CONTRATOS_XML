using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_notificacionsanciones
    {
        public string Id_obligacion { get; set; }
        public string Id_sancion{ get; set; }
        public string Sancion { get; set; }
        public string Clausula { get; set; }
        public string Obligacion { get; set; }
        public string Responsables { get; set; }
        public string Areas { get; set; }
    }
    public class tbl_plan_por_obligacion
    {
        public string identificador { get; set; }
        public string descripcion { get; set; }
        public string periodo { get; set; }
        public DateTime fecha_ejecucion { get; set; }
    }
}
