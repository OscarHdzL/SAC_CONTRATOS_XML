using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class DropDownList
	{
		public String Value { get; set; }
		public String Text { get; set; }
	}
	public class producto_servicio_pe
	{
		public Guid id { get; set; }
		public Guid tbl_dependencia_id { get; set; }
		public String producto_servicio { get; set; }
		public String clave_producto { get; set; }
		public String elemento { get; set; }
		public String elemento_desc { get; set; }
		public Guid tbl_unidad_medida_id { get; set; }
		public DateTime inclusion { get; set; }
		public Boolean activo { get; set; }
		public String comentario { get; set; }
		public Guid tbl_tipo_id { get; set; }
		public Guid tbl_plan_entrega_producto_id { get; set; }
	}
}
