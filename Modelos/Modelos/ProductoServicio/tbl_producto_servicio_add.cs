using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class tbl_producto_servicio_add
	{
		public int p_opt { get; set; }
		public String p_id { get; set; }
		public Guid p_tbl_dependencia_id { get; set; }
		public String p_producto_servicio { get; set; }
		public String p_clave_producto { get; set; }
		public String p_elemento { get; set; }
		public String p_elemento_desc { get; set; }
		public Guid p_tbl_unidad_medida_id { get; set; }
		public Byte p_activo { get; set; }
		public String p_comentario { get; set; }
		public String p_tbl_tipo_id { get; set; }
	}

}
