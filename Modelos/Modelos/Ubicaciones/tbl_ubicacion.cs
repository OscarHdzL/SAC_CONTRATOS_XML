using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class UbicacionEjecutores_add { 
		public tbl_ubicacion_add Ubicacion { get; set; }
		public ubicacionEjecutores Ejecutores { get; set; }
	}
	public class ubicacionEjecutores 
	{ 
		public int p_opt { get; set; }
		public Guid p_tbl_ubicacion_id { get; set; }
		public String p_str_ids { get; set; }

	}

	public class ubicacion_ejecutor
	{
		public int p_opt { get; set; }
		public Guid p_tbl_ubicacion_servidor_id { get; set; }
		public Guid p_tbl_ubicacion_id { get; set; }
		public Guid p_tbl_rol_usuario_id { get; set; }

	}

	public class tbl_ubicacion_add
	{
		public int p_opt { get; set; }
		public Guid p_id { get; set; }
		public Guid p_tbl_dependencia_id { get; set; }
		public Guid p_tbl_ciudad_id { get; set; }
		public String p_clave { get; set; }
		public String p_unidad { get; set; }
		public String p_direccion { get; set; }
		public String p_referencia { get; set; }
		public String p_telefono { get; set; }
		public Boolean p_activo { get; set; }
		public String p_dias_atencion { get; set; }
		public String p_horario_atencion { get; set; }
		public Guid p_tbl_rol_usuario_id { get; set; }
		
	}
	public class tbl_ubicacion_input 
	{ 
		public Guid idparameter { get; set; }
		public String tipo { get; set; }

	}

	public class validar_ubicacion_ligada
	{
		public Boolean ubicacion_ligada { get; set; }
	}

	public class tbl_ubicacion_output
	{
		public Guid id { get; set; }
		public String clave { get; set; }
		public String unidad { get; set; }
		public String direccion { get; set; }
		public String telefono { get; set; }
		public String referencia { get; set; }
		public String dias_atencion { get; set; }
		public String horario_atencion { get; set; }
		public Guid tbl_ubicacion_servidor_id { get; set; }
		public Guid responsable_ubicacion_id { get; set; }
		public String responsable_ubicacion { get; set; }
		public Guid tbl_rol_usuario_id { get; set; }
		public String tbl_rol_usuario { get; set; }
		public Boolean responsable { get; set; }
		public Guid tbl_dependencia_id { get; set; }
		public String dependencia { get; set; }

		public Guid tbl_estado_id { get; set; }
		public String tbl_estado_nombre { get; set; }
		public Guid tbl_ciudad_id { get; set; }
		public String tbl_ciudad_nombre { get; set; }

	}
	public class tbl_ubicacion_interfaz: tbl_ubicacion_output
	{
		public List<DropDownList> Responsables { get; set; }
	}
	public class tbl_ubicacion
	{
		public Guid id { get; set; }
		public Guid tbl_servidor_publico_id { get; set; }
		public Guid tbl_dependencia_id { get; set; }
		public String clave { get; set; }
		public String unidad { get; set; }
		public String direccion { get; set; }
		public String referencia { get; set; }
		public String telefono { get; set; }
		public DateTime inclusion { get; set; }
		public Boolean activo { get; set; }
		public String dias_atencion { get; set; }
		public String horario_atencion { get; set; }
		public Guid tbl_ciudad_id { get; set; }
	}

	public class plan_entrega_ubicacion 
	{
		public Guid tbl_ubicacion_plan_entrega_id { get; set; }
		public Guid tbl_ubicacion_id { get; set; }
		public String tbl_ubicacion_clave { get; set; }
		public String tbl_ubicacion_unidad { get; set; }
		public String tbl_ubicacion_direccion { get; set; }
		public String tbl_ubicacion_referencia { get; set; }
		public String tbl_ubicacion_telefono { get; set; }
		public String tbl_ubicacion_detalle_actividad { get; set; }
		public DateTime tbl_ubicacion_inclusion { get; set; }
		public String tbl_ubicacion_dias_atencion { get; set; }
		public String tbl_ubicacion_horario_atencion { get; set; }
		public Guid tbl_contrato_servidor_resp_id { get; set; }
		public String tbl_contrato_servidor_resp_str { get; set; }
		public int total_productos { get; set; }
	}
	public class token_ubicacion_PE
	{
		public String token { get; set; }
	}
	public class lista_plan_entrega_ubicacion
	{
		public plan_entrega_ubicacion ubicacion { get; set; }
		public String token { get; set; }
	}
}
