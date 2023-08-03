using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.Dependencia
{
    public class tbl_partida_list
    {       
        public string id { get; set; }
        public string numero { get; set; }
        public string descripcion { get; set; }
        public string chek { get; set; }
    }
    public class tbl_partida
    {
        public string id { get; set; }
        public string tbl_dependencia_id { get; set; }
        public string tbl_partida_id { get; set; }
        public string tbl_ejercicio_id { get; set; }

    }
    public class tbl_partida_upd 
    {
        public Guid tbl_partida_dependencia_id { get; set; }
        public Double monto_asignado { get; set; }

    }
    public class suma_c_areas_padre 
    {
        public Double monto_total { get; set; }
    }
}
