using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.RegSolDoc
{
   public class tbl_contrato_solicitud_docto
    {
        public string id { get; set; }
        public string nombre_documento { get; set; }       
        public DateTime fecha_solicitud { get; set; }
        public DateTime fecha_entrega { get; set; }
        public string tipo_entregable { get; set; }
        public string observacion { get; set; }
        public string correo_solicitud { get; set; }
        public DateTime inclusion { get; set; }
        public bool estatus { get; set; }
        public string tbl_contrato_servidor_resp_id { get; set; }
        public string nombre_responsable { get; set; }
        public string area_id_responsable { get; set; }
        public string area_responsable { get; set; }
        public string correo_responsable { get; set; }
        public string tbl_contrato_id { get; set; }




    }
}
