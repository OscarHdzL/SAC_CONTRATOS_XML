using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_convocatoria_criterio
	{
		public int p_opt { get; set; }					
		public Guid p_id { get; set; }
		public Guid p_tbl_convocatoria_id { get; set; }
		public Guid p_tbl_tipocriterio_id { get; set; }
		public Guid p_tbl_estatus_obligacion_id { get; set; }
		public String p_criterio { get; set; }
		public String p_evaluacion { get; set; }
	}
	public class tbl_convocatoria_criterio_lista
	{
		public Guid id { get; set; }
		public Guid tbl_convocatoria_id { get; set; }
		public Guid tbl_tipocriterio_id { get; set; }
		public String criterio { get; set; }
		public String evaluacion { get; set; }
		public String folio { get; set; }
		public String tipo_criterio { get; set; }
	}
}