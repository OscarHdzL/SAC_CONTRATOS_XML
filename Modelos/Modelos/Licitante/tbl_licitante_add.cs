using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class tbl_licitante_add
	{
		
		public int p_opt { get; set; }
		public String p_id { get; set; }
		public String p_tbl_solicitud_id { get; set; }
		public String p_razon_social { get; set; }
		public String p_rep_legal_nombre { get; set; }
		public String p_rep_legal_ap_paterno { get; set; }
		public String p_rep_legal_ap_materno { get; set; }
		public String p_rfc { get; set; }
		public byte p_es_proveedor { get; set; }

	}	
}

