using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_modalidad
	{
		
		public Guid id { get; set; }
		public Guid tbl_solicitud_id { get; set; }
		public Guid tbl_tipo_modalidad_id { get; set; }
		public Guid tbl_tipo_licitacion_id { get; set; }
		public Boolean falta_documentacion { get; set; }
		public Boolean requiere_justificacion { get; set; }
		public String token { get; set; }
		public Boolean estatus { get; set; }
		public DateTime inclusion { get; set; }
		public Boolean visita_sitio { get; set; }
		public Boolean aclaraciones { get; set; }
		public String sigla_licitacion { get; set; }

	}
}
