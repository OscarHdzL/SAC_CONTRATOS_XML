using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class tbl_tipo_acuerdo
	{
		public Guid id { get; set; }
		public String tipo_acuerdo { get; set; }

	}

	public class tbl_tipo_acuerdo_add
	{
		public int p_opt { get; set; }
		public Guid p_id { get; set; }
		public string p_tipo_acuerdo { get; set; }

	}
}
