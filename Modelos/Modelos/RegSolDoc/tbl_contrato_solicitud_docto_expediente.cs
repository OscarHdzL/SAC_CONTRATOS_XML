using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.RegSolDoc
{
   public class tbl_contrato_solicitud_docto_expediente
    {
        public string id_solicitud { get; set; }
        public string nombre_documento { get; set; }       
        public string nombre_responsable { get; set; }
        public string correo_responsable { get; set; }
        public string observacion { get; set; }
        public string tbl_gestion_expediente_contrato_id { get; set; }
        public string token_doc { get; set; }


    }
}
