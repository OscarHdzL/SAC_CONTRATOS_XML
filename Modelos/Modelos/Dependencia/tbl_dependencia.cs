using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.Dependencia
{
    public class tbl_dependencia
    {
        public string id { get; set; }
        public string dependencia { get; set; }
        public string tbl_ciudad_id { get; set; }
        public string tbl_instancia_id { get; set; }
        public int num_hijas { get; set; }
        public int puestos { get; set; }
        public string id_estado { get; set; }
    }

    public class tbl_dependencia_x_permiso
    {
        public string id { get; set; }
        public string dependencia { get; set; }
        public string tbl_ciudad_id { get; set; }
        public string tbl_instancia_id { get; set; }
        public int num_hijas { get; set; }
        public int puestos { get; set; }
        public string id_estado { get; set; }
        public int permiso { get; set; }
    }
}
