using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class tbl_matriz_riesgo_add
	{
		public int p_opt { get; set; }
		public Guid p_id { get; set; }
		public string p_riesgo { get; set; }
		public Guid p_tbl_nivel_riesgo_id { get; set; }
		public int p_probabilidad { get; set; }
		public int p_impacto { get; set; }
		public String p_prioridad { get; set; }
		public Guid p_tbl_link_obligacion_id { get; set; }
		public Guid p_tbl_tipo_respuesta_id { get; set; }
		public String p_accion { get; set; }
		public String p_respuesta { get; set; }
		public byte p_estatus { get; set; }
	}
	public class tbl_matriz_riesgo
	{
		public Guid id { get;set;}
		public String riesgo{ get;set;}
		public String nivel_riesgo { get; set; }
		public Guid	tbl_nivel_riesgo_id{get;set;}
		public int probabilidad{get;set;}
		public int impacto{get;set;}
		public Double prioridad{get;set;}
		public Guid tbl_link_obligacion_id{get;set;}
		public Guid tbl_tipo_respuesta_id{get;set;}
		public String tipo_respuesta { get; set; }
		public String accion{get;set;}
		public Boolean estatus{get;set;}
	}
}
