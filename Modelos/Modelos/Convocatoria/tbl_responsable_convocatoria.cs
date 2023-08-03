using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_responsable_convocatoria
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public Guid p_tbl_servidor_publico_id { get; set; }
        public String p_token { get; set; }
        public Guid p_tbl_convocatoria_id { get; set; }
    }
    public class tbl_responsable_convocatoria_lista
    {
        public Guid id { get; set; }
        public Guid tbl_servidor_publico_id { get; set; }
        public String token { get; set; }
        public DateTime inclusion { get; set; }
        public Guid tbl_convocatoria_id { get; set; }
        public String servidor { get; set; }
        public String folio { get; set; }
        public Guid id_area { get; set; }
        public String area { get; set; }
    }
}
