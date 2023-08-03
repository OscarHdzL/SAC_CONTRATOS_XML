using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_plan_monitoreo_estado
    {
        public string id { get; set; }
        public string descripcion { get; set; }
        public DateTime inclusion { get; set; }
        public bool activo { get; set; }

    }
}
