using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_cotizacion_solicitud
    {   
        public string id { get; set; }
        public string tbl_solicitud_documento_adjunto_id { get; set; }
        public string razon_social { get; set; }
        public string nombre_documento { get; set; }
        public string tipo_documento { get; set; }
        public string token { get; set; }
    }    

    public class tbl_cotizacion_sol_crud
    {
        public int p_opt { get; set; }
        public string p_id { get; set; }
        public string p_tbl_proveedor_id { get; set; }
        public string p_tbl_solicitud_documento_adjunto_id { get; set; }
        public string p_tbl_tipo_documento_id { get; set; }
        public string p_tbl_solicitud_id { get; set; }
        public string p_nom_documento { get; set; }
    }
}
