using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_pc_mensaje
    {
        public string id { get; set; }
        public string fuenteinfo_tbl_responsable_servidor_resp_id { get; set; }
        public string tbl_tipoinformacion_id { get; set; }
        public string tbl_tipoaudiencia_id { get; set; }
        public string tbl_Canal_id { get; set; }
        public string destinatario_tbl_responsable_servidor_resp_id { get; set; }
        public string mensaje { get; set; }
        public DateTime inclusion { get; set; }
        public bool estatus { get; set; }
    }
}
