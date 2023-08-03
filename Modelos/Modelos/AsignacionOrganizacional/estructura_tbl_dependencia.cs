using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class estructura_tbl_dependencia
    {
        public Guid id { get; set; }
        public String dependencia { get; set; }
        public int hijos { get; set; }
    }
}
