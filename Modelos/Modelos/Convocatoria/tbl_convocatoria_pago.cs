using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_convocatoria_pago
	{
        public int p_opt { get; set; }					
		public Guid p_id { get; set; }
		public Guid p_tbl_convocatoria_id { get; set; }
		public Guid p_tbl_estatus_obligacion_id { get; set; }
		public String p_condicion_pago { get; set; }
		public String p_metodo_pago { get; set; }
		public String p_tipo_pago { get; set; }
		public String p_porcentaje_cantidad { get; set; }

	}
	public class tbl_convocatoria_pago_lista
	{
		public Guid id { get; set; }
		public Guid tbl_convocatoria_id { get; set; }
		public String condicion_pago { get; set; }
		public String metodo_pago { get; set; }
		public String tipo_pago { get; set; }
		public String porcentaje_cantidad { get; set; }
		public String folio { get; set; }
	}
}
