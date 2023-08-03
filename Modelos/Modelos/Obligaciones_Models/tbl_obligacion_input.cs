using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class tbl_obligacion_input_conatiner
	{ 
		public tbl_obligacion_input tbl_obligacion { get; set; }
		public tbl_link_obligacion_input tbl_link_obligacion { get; set; }
		public tbl_area_obligacion_input tbl_area_obligacion { get; set; }
		public tbl_responsable_obligacion_input tbl_responsable_obligacion { get; set; }

	}
	public class tbl_obligacion_input
	{
		public int p_opt { get; set; }
		public Guid p_id { get; set; }
		public String p_clausula { get; set; }
		public int p_nivel_servicio { get; set; }
		public String p_forma_aplicacion { get; set; }
		public String p_comentarios { get; set; }
		public String p_obligacion { get; set; }
		public double p_monto { get; set; }
		public double p_porcentaje { get; set; }
		public String p_tbl_tipo_prioridad_id { get; set; }
	}
	public class tbl_link_obligacion_input
	{
		public int p_opt { get; set; }
		public Guid p_id { get; set; }
		public Guid p_tbl_obligacion_id { get; set; }
		public Guid p_tbl_contrato_id { get; set; }
		public Guid p_tbl_tipo_obligacion_id { get; set; }
		public Guid p_tbl_sancion_obligacion_id { get; set; }
		public Guid p_tbl_periodo_id { get; set; }
		public Guid p_tbl_producto_servicio_id { get; set; }
		public byte p_estatus { get; set; }
		public Guid p_tbl_tipo_aplicacion_id { get; set; }
		public String p_str_areas { get; set; }
		public String p_str_responsables { get; set; }

	}
	public class tbl_area_obligacion_input
	{ 
		public Guid p_id_obligacion_id { get; set; }
		public String p_str_areas { get; set; }
	}
	public class tbl_responsable_obligacion_input
	{
		public Guid p_id_obligacion_id { get; set; }
		public String p_str_responsables { get; set; }
	}
}
