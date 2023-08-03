using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.GestionExpediente
{
    public class tbl_gestion_expediente_contrato_add
    {
        public int p_opt { get; set; }
        public String p_id { get; set; }
        public String p_tbl_contrato_solicitud_docto_id { get; set; }
        public String p_token_doc { get; set; }
        public String p_inclusion { get; set; }
        public byte p_estatus { get; set; }

    }
}
