using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class vs_plan_entrega
	{
		public Guid tbl_plan_entrega_id { get; set; }
		public Guid tbl_contrato_servidor_resp_id { get; set; }
		public String identificador { get; set; }
		public String periodo { get; set; }
		public String descripcion { get; set; }
		public DateTime fecha_ejecucion { get; set; }
		public DateTime tbl_plan_entrega_inclusion { get; set; }
		public Boolean tbl_plan_entrega_activo { get; set; }
		public String tipo_entrega { get; set; }
		public Guid tbl_ubicacion_id { get; set; }
		public Guid tbl_plan_entrega_producto_id { get; set; }
		public Guid tbl_contrato_producto_id { get; set; }
		public Guid tbl_ubicacion_plan_entrega_id { get; set; }
		public Boolean tbl_plan_entrega_producto_estatus { get; set; }
		public DateTime tbl_plan_entrega_producto_inclusion { get; set; }
		public int cantidad { get; set; }
		public String detalle_actividad { get; set; }
		public String tipo { get; set; }
		public Boolean cumplio { get; set; }

		public Guid tbl_contrato_id { get; set; }
		public Guid tbl_plan_entrega_ejecutor_id { get; set; }
		public Double? monto { get; set; }
		public Double? monto_iva { get; set; }
		public Double? total { get; set; }
		public string Ejecutor_nombre { get; set; }

	}
	public class vs_plan_entrega_ejec
	{
		public Guid tbl_plan_entrega_id { get; set; }
		public Guid tbl_contrato_servidor_resp_id { get; set; }
		public String identificador { get; set; }
		public String periodo { get; set; }
		public String descripcion { get; set; }
		public DateTime fecha_ejecucion { get; set; }
		public DateTime tbl_plan_entrega_inclusion { get; set; }
		public Boolean tbl_plan_entrega_activo { get; set; }
		public String tipo_entrega { get; set; }
		public Boolean cumplio_pe { get; set; }
		public Boolean ejecutado { get; set; }
		public Guid tbl_ubicacion_id { get; set; }
		public Guid tbl_plan_entrega_producto_id { get; set; }
		public Guid tbl_contrato_producto_id { get; set; }
		public Guid tbl_ubicacion_plan_entrega_id { get; set; }
		public Boolean tbl_plan_entrega_producto_estatus { get; set; }
		public DateTime tbl_plan_entrega_producto_inclusion { get; set; }
		public int cantidad { get; set; }
		public String detalle_actividad { get; set; }
		public String tipo { get; set; }
		public Boolean cumplio { get; set; }

		public Guid tbl_contrato_id { get; set; }
		public Guid tbl_plan_entrega_ejecutor_id { get; set; }


	}

	public class vs_plan_entrega_detalle_producto
	{
		public Guid tbl_plan_entrega_id { get; set; }
		public Guid tbl_contrato_servidor_resp_id { get; set; }
		public String Responsable_PE { get; set; }
		public String identificador { get; set; }
		public String periodo { get; set; }
		public String descripcion { get; set; }
		public DateTime? fecha_ejecucion { get; set; }
		public DateTime? tbl_plan_entrega_inclusion { get; set; }
		public Boolean tbl_plan_entrega_activo { get; set; }
		public String tipo_entrega { get; set; }
		public Guid tbl_ubicacion_id { get; set; }
		public Guid tbl_plan_entrega_producto_id { get; set; }
		public Guid tbl_contrato_producto_id { get; set; }
		public Guid tbl_ubicacion_plan_entrega_id { get; set; }
		public Boolean? tbl_plan_entrega_producto_estatus { get; set; }
		public DateTime? tbl_plan_entrega_producto_inclusion { get; set; }
		public int? cantidad { get; set; }
		public String detalle_actividad { get; set; }
		public String tipo { get; set; }
		public Boolean? cumplio { get; set; }
		public Guid tbl_contrato_id { get; set; }
		public Guid tbl_plan_entrega_ejecutor_id { get; set; }
		public Double? monto { get; set; }
		public Double? monto_iva { get; set; }
		public Double? total { get; set; }
		public string Ejecutor_nombre { get; set; }
		public Guid tbl_producto_servicio_id { get; set; }
		public String clave_producto { get; set; }
		public String elemento { get; set; }
		public String clave_ubicacion { get; set; }
		public String unidad_ubicacion { get; set; }
		public String direccion_ubicacion { get; set; }

	}

	public class plan_entrega_detalle_producto_cuerpo 
	{ 
		public plan_entrega_detalle_producto_header header { get; set; }
		public List<plan_entrega_detalle_producto_ubicacion> ubicaciones { get; set; }
	}

	public class plan_entrega_detalle_producto_header 
	{
		public Guid tbl_plan_entrega_id { get; set; }
		public Guid tbl_contrato_servidor_resp_id { get; set; }
		public String Responsable_PE { get; set; }
		public String identificador { get; set; }
		public String periodo { get; set; }
		public String descripcion { get; set; }
		public DateTime? fecha_ejecucion { get; set; }
		public DateTime? tbl_plan_entrega_inclusion { get; set; }
		public Boolean? tbl_plan_entrega_activo { get; set; }
		public String tipo_entrega { get; set; }
	}
	public class plan_entrega_detalle_producto_ubicacion 
	{
		public Guid tbl_ubicacion_id { get; set; }
		public String Ejecutor_nombre { get; set; }
		public String clave_ubicacion { get; set; }
		public String unidad_ubicacion { get; set; }
		public String direccion_ubicacion { get; set; }
		public Guid tbl_plan_entrega_ejecutor_id { get; set; }
		public List<plan_entrega_detalle_producto> listado_productos { get; set; }
	}
	public class plan_entrega_detalle_producto
	{ 
		public Guid tbl_plan_entrega_producto_id { get; set; }
        public Guid tbl_contrato_producto_id { get; set; }
        public Guid tbl_ubicacion_plan_entrega_id { get; set; }
        public Boolean? tbl_plan_entrega_producto_estatus { get; set; }
        public DateTime? tbl_plan_entrega_producto_inclusion { get; set; }
        public int? cantidad { get; set; }
        public String detalle_actividad { get; set; }
        public String tipo { get; set; }
        public Boolean? cumplio { get; set; }
        public Guid tbl_contrato_id { get; set; }
        public Double? monto { get; set; }
        public Double? monto_iva { get; set; }
        public Double? total { get; set; }
        public Guid tbl_producto_servicio_id { get; set; }
        public String clave_producto { get; set; }
        public String elemento { get; set; }
	
	}

}
