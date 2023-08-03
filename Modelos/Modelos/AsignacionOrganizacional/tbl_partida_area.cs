using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_partida_area
    {
        public int p_opt { get; set; }
        public String p_id { get; set; }
        public String p_tbl_area_id { get; set; }
        public String p_tbl_partida_id { get; set; }
        public String p_tbl_ejercicio_id { get; set; }
        public String p_id_propietario { get; set; }
        public Double p_monto_planeado { get; set; }
        public Double p_monto_asignado { get; set; }
        public Double p_monto_ejercido { get; set; }
        public Double p_monto_devengado { get; set; }
        public int p_estatus_partida { get; set; }
    }
    public class existe_partida 
    {
        public Guid tbl_partida_area_id { get; set; } 
    }
    public class monto_asignacion
    {
        public Double monto_asignado { get; set; }
    }
}
