using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class vs_siderbar
    {
        public string tbl_elemento_siderbar_id { get; set; }        
        public string subordinado { get; set; }
        public string tag { get; set; }
        public string nombre { get; set; }
        public string valor { get; set; }
        public string tipo { get; set; }
        public string clase { get; set; }
        public Nullable<bool> tiene_contrato { get; set; }
        public string controlador { get; set; }
        public string accion { get; set; }
        public int orden { get; set; }
        public int generacion { get; set; }
        public string id_rol { get; set; }

    }
}
