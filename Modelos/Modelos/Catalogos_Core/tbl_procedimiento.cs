using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_procedimiento
    {
        public Guid id { get; set; }
        public String procedimiento { get; set; }
        public String sigla { get; set; }
        public Boolean? activo { get; set; }
    }

    public class tbl_procedimiento_add
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public String p_procedimiento { get; set; }
        public String p_sigla { get; set; }
    }


}
