using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class sp_modalidad
	{
		public int p_opt { get; set; }
		public Guid p_id { get; set; }
		public Guid p_tbl_solicitud_id { get; set; }
		public Guid p_tbl_tipo_modalidad_id { get; set; }
		public Guid p_tbl_tipo_licitacion_id { get; set; }
		public Boolean p_falta_documentacion { get; set; }
		public Boolean p_requiere_justificacion { get; set; }
		public String p_token { get; set; }
		public Boolean p_estatus { get; set; }
		public DateTime  p_inclusion { get; set; }
		public Boolean p_visita_sitio { get; set; }
		public Boolean p_aclaraciones { get; set; }

	}
}
