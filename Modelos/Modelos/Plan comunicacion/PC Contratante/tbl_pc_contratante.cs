using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{ 
    public class tbl_pc_contratante
    {
        public string id { get; set; }
        public string tbl_responsable_servidor_resp_id { get; set; }
        public string tbl_ubicaciones_id { get; set; }
        public string tbl_tipoaudiencia_id { get; set; }
        public DateTime inclusion { get; set; }
        public bool estatus { get; set; }
    }
}
