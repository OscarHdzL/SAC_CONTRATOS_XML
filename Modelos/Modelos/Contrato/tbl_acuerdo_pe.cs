using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.Contrato
{
    public class tbl_acuerdo_pe
    {
			public String id { get; set; }
			public String contrato_id { get; set; }
			public String tipo_acuerdo_id { get; set; }
			public String tipo_acuerdo { get; set; }
			public String responsable_id { get; set; }
			public String responsable { get; set; }
			public String acuerdo { get; set; }
			public DateTime fecha_registro { get; set; }
			public DateTime fecha_compromiso { get; set; }
			public DateTime fecha_cierre { get; set; }
			public bool estatus { get; set; }
			public String estatus_acuerdo { get; set; }
			public String comentario { get; set; }
		}
}
