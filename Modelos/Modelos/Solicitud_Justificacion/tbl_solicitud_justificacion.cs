using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos { 
    public class tbl_solicitud_justificacion
	{
		public int p_opt { get; set; }
		public Guid p_id { get; set; }
		public Guid p_tbl_solicitud_id { get; set; }
		public string p_justificacion { get; set; }
		public string p_comentarios { get; set; }
		public byte p_procede { get; set; }

	}
}
