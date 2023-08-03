using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_sanciones
    {

        public string id { get; set; }
        public String sansion { get; set; }
        public DateTime inclusion { get; set; }

    }

    public class tbl_tipo_sancion_add
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public String p_sancion { get; set; }
    }
}
