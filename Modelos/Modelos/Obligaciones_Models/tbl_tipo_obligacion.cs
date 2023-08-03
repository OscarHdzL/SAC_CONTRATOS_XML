using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class tbl_tipo_obligacion
    {
		public String id { get; set; }
		public String tipo_obligacion { get; set; }
		public DateTime inclusion { get; set; }
	}
	public class tbl_tipo_obligacion_add
    {
		public int p_opt { get; set; }
		public Guid p_id { get; set; }
		public string p_tipo_obligacion { get; set; }
	}

	public class verificar_oblig {
		public bool estado { get; set; }
	} 
}
