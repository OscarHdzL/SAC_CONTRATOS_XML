using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class tbl_obligacion_unitario
	{
		public String tbl_link_obligacion_id { get; set; }
		public String tbl_obligacion_id { get; set; }
		public String clausula { get; set; }
		public int? nivel_servicio { get; set; }
		public String forma_aplicacion { get; set; }
		public String comentarios { get; set; }
		public DateTime inclusion { get; set; }
		public String obligacion { get; set; }
		public double monto { get; set; }
		public double porcentaje { get; set; }
		public String tbl_tipo_obligacion_id { get; set; }
		public String tipo_obligacion { get; set; }
		public String tbl_sancion_obligacion_id { get; set; }
		public String sansion { get; set; }
		public String tbl_tipo_aplicacion_id { get; set; }
		public String tipo_aplicacion { get; set; }
		public String tbl_periodo_id { get; set; }
		public String periodo { get; set; }
		public Guid tbl_producto_servicio_id { get; set; }
		public String tbl_tipo_prioridad_id { get; set; }
		public String tbl_tipo_prioridad_nombre { get; set; }
	}
	public class tbl_obligacion
	{
		public String tbl_link_obligacion_id { get; set; }
		public String tbl_obligacion_id { get; set; }
		public String clausula { get; set; }
		public int? nivel_servicio { get; set; }
		public String forma_aplicacion { get; set; }
		public String comentarios { get; set; }
		public DateTime inclusion { get; set; }
		public String obligacion { get; set; }
		public double monto { get; set; }
		public double porcentaje { get; set; }
		public String tbl_tipo_obligacion_id { get; set; }
		public String tipo_obligacion { get; set; }
		public String tbl_sancion_obligacion_id { get; set; }
		public String sansion { get; set; }
		public String tbl_tipo_aplicacion_id { get; set; }
		public String tipo_aplicacion { get; set; }
		public String tbl_periodo_id { get; set; }
		public String periodo { get; set; }
		public String tbl_tipo_prioridad_id { get; set; }
		public String tbl_tipo_prioridad_nombre { get; set; }
	}

	public class tbl_obligacion_producto
	{
		public String tbl_link_obligacion_id { get; set; }
		public String tbl_obligacion_id { get; set; }
		public String clausula { get; set; }
		public int? nivel_servicio { get; set; }
		public String forma_aplicacion { get; set; }
		public String comentarios { get; set; }
		public DateTime inclusion { get; set; }
		public String obligacion { get; set; }
		public double monto { get; set; }
		public double porcentaje { get; set; }
		public String tbl_tipo_obligacion_id { get; set; }
		public String tipo_obligacion { get; set; }
		public String tbl_sancion_obligacion_id { get; set; }
		public String sansion { get; set; }
		public String tbl_tipo_aplicacion_id { get; set; }
		public String tipo_aplicacion { get; set; }
		public String tbl_periodo_id { get; set; }
		public String periodo { get; set; }
		public String tbl_tipo_prioridad_id { get; set; }
		public String tbl_tipo_prioridad_nombre { get; set; }
		public String producto_servicio_id { get; set; }
		public String producto_servicio_nombre { get; set; }
		public String producto_servicio_clave { get; set; }

	}

	public class tbl_obligacion_detalle
	{
		public tbl_obligacion Obligacion { get; set; }
		public List<DropDownList> Areas { get; set; }
		public List<DropDownList> Responsables { get; set; }

	}
	public class tbl_obligacion_cls{
		public Guid id { get; set; }
		public String clausula { get; set; }
		public int nivel_servicio { get; set; }
		public String forma_aplicacion { get; set; }
		public String comentarios { get; set; }
		public DateTime inclusion { get; set; }
		public String obligacion { get; set; }
		public Double monto { get; set; }
		public double porcentaje { get; set; }
	}

	public class tbl_obligacion_cls_PE
	{
		public Guid id { get; set; }
		public String clausula { get; set; }
		public int nivel_servicio { get; set; }
		public String forma_aplicacion { get; set; }
		public String comentarios { get; set; }
		public DateTime inclusion { get; set; }
		public String obligacion { get; set; }
		public Double monto { get; set; }
		public double porcentaje { get; set; }
		public Guid tbl_link_obligaciones_id { get; set; }
		public string tbl_tipo_prioridad_id { get; set; }
		public string tbl_tipo_prioridad_nombre { get; set; }
	}


}
