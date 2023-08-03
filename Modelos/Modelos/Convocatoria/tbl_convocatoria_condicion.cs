using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_convocatoria_condicion
    {
        public int p_opt { get; set; }                    
        public Guid p_id { get; set; }
        public Guid p_tbl_convocatoria_id { get; set; }
        public Guid p_tbl_estatus_obligacion_id { get; set; }
        public String p_periodo { get; set; }
        public String p_condicion { get; set; }
    }
    public class tbl_convocatoria_condicion_lista
    {
        public Guid id { get; set; }
        public Guid tbl_convocatoria_id { get; set; }
        public String periodo { get; set; }
        public String condicion { get; set; }
        public String folio { get; set; }
    }
}
