using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class ReporteSancionesConsulta
    {
        public String tbl_plan_entrega_id { get; set; }
        public String tbl_contrato_servidor_resp_id { get; set; }
        public String identificador { get; set; }
        public String periodo { get; set; }
        public String descripcion { get; set; }
        public DateTime? fecha_ejecucion { get; set; }
        public int? dias_restantes_ejecucion { get; set; }
        public DateTime? tbl_plan_entrega_inclusion { get; set; }
        public Boolean? tbl_plan_entrega_activo { get; set; }
        public String tipo_entrega { get; set; }
        public Boolean? cumplio_pe { get; set; }
        public Boolean? ejecutado { get; set; }
        public String tbl_ubicacion_id { get; set; }
        public String tbl_ubicacion_clave { get; set; }
        public String tbl_ubicacion_unidad { get; set; }
        public String tbl_plan_entrega_producto_id { get; set; }
        public String tbl_contrato_producto_id { get; set; }
        public String tbl_ubicacion_plan_entrega_id { get; set; }
        public Boolean? tbl_plan_entrega_producto_estatus { get; set; }
        public DateTime? tbl_plan_entrega_producto_inclusion { get; set; }
        public int? cantidad { get; set; }
        public String detalle_actividad { get; set; }
        public String tipo { get; set; }
        public Boolean? cumplio { get; set; }
        public String tbl_contrato_id { get; set; }
        public String tbl_rol_usuario_id { get; set; }
        public String tbl_plan_entrega_ejecutor_id { get; set; }
        public String tbl_ubicacion_servidor_id { get; set; }
        public String tbl_link_obligacion_id { get; set; }
        public String tbl_obligacion_id { get; set; }
        public String clausula { get; set; }
        public int? nivel_servicio { get; set; }
        public String forma_aplicacion { get; set; }
        public String comentarios { get; set; }
        public DateTime? inclusion { get; set; }
        public String obligacion { get; set; }
        public Double? monto { get; set; }
        public Double? porcentaje { get; set; }
        public String tbl_tipo_obligacion_id { get; set; }
        public String tipo_obligacion { get; set; }
        public String tbl_sancion_obligacion_id { get; set; }
        public String sansion { get; set; }
        public String tbl_periodo_id { get; set; }
        public String tbl_periodo_periodo { get; set; }
        public String tbl_tipo_prioridad_id { get; set; }
        public String tbl_tipo_prioridad_nombre { get; set; }
        public String producto_servicio_id { get; set; }
        public String producto_servicio_nombre { get; set; }
        public String producto_servicio_clave { get; set; }
        public int? obligacion_cumplida { get; set; }

    }

    public class ReporteSancionesBody
    {
        public String tbl_contrato_id { get; set; }
        public String tbl_plan_entrega_id { get; set; }
        public String tbl_contrato_servidor_resp_id { get; set; }
        public String identificador { get; set; }
        public String periodo { get; set; }
        public String descripcion { get; set; }
        public DateTime? fecha_ejecucion { get; set; }
        public int? dias_restantes_ejecucion { get; set; }
        public DateTime? tbl_plan_entrega_inclusion { get; set; }
        public Boolean? tbl_plan_entrega_activo { get; set; }
        public String tipo_entrega { get; set; }
        public Boolean? cumplio_pe { get; set; }
        public Boolean? ejecutado { get; set; }
        public List<ReporteSancionesUbicaciones> Ubicaciones { get; set; }

    }

    public class ReporteSancionesUbicaciones 
    {
        public String tbl_ubicacion_id { get; set; }
        public String tbl_ubicacion_clave { get; set; }
        public String tbl_ubicacion_unidad { get; set; }
        public List<ReporteSancionesProducto> Productos { get; set; }

    }

    public class ReporteSancionesProducto
    {
        public String tbl_plan_entrega_producto_id { get; set; }
        public String tbl_contrato_producto_id { get; set; }
        public String tbl_ubicacion_plan_entrega_id { get; set; }
        public Boolean? tbl_plan_entrega_producto_estatus { get; set; }
        public DateTime? tbl_plan_entrega_producto_inclusion { get; set; }
        public int? cantidad { get; set; }
        public String detalle_actividad { get; set; }
        public String tipo { get; set; }
        public Boolean? cumplio { get; set; }
        public String producto_servicio_id { get; set; }
        public String producto_servicio_nombre { get; set; }
        public String producto_servicio_clave { get; set; }
        public List<ReporteSancionesObligacion> Obligaciones { get; set; }
    }

    public class ReporteSancionesObligacion
    {
        //public String tbl_rol_usuario_id { get; set; }
        //public String tbl_plan_entrega_ejecutor_id { get; set; }
        //public String tbl_ubicacion_servidor_id { get; set; }
        public String tbl_link_obligacion_id { get; set; }
        public String tbl_obligacion_id { get; set; }
        public String clausula { get; set; }
        public int? nivel_servicio { get; set; }
        public String forma_aplicacion { get; set; }
        public String comentarios { get; set; }
        public DateTime? inclusion { get; set; }
        public String obligacion { get; set; }
        public Double? monto { get; set; }
        public Double? porcentaje { get; set; }
        public String tbl_tipo_obligacion_id { get; set; }
        public String tipo_obligacion { get; set; }
        public String tbl_sancion_obligacion_id { get; set; }
        public String sansion { get; set; }
        public String tbl_periodo_id { get; set; }
        public String tbl_periodo_periodo { get; set; }
        public String tbl_tipo_prioridad_id { get; set; }
        public String tbl_tipo_prioridad_nombre { get; set; }
        public int? obligacion_cumplida { get; set; }

    }

}
