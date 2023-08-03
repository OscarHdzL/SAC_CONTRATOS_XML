using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class tbl_licitante
	{
		
		public String id { get; set; }
		public String tbl_solicitud_id { get; set; }
		public String licitante { get; set; }
		public String rep_nombre { get; set; }
		public String rep_paterno { get; set; }
		public String rep_materno { get; set; }
		public String rfc { get; set; }
		public Boolean es_proveedor { get; set; }
		public DateTime inclusion { get; set; }
		public Boolean estatus { get; set; }

	}	
}

