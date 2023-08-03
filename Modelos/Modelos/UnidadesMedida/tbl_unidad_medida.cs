using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class tbl_unidad_medida
	{
		public Guid id { get; set; }
		public String unidad_medida { get; set; }
		public String clave { get; set; }
	}
	public class tbl_unidad_medida_add 
	{
		public int p_opt { get; set; }
		public Guid p_id { get; set; }
		public String p_unidad_medida { get; set; }
		public String p_clave { get; set; }
	}
}
