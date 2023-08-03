using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class estructura_tbl_area
    {
        public Guid id { get; set; }
        public Guid tbl_dependencia_id { get; set; }
        public String area { get; set; }
        public String id_area_padre { get; set; }
        public int hijos { get; set; }
    }
}
