using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Modelos.Modelos
{
    public class EstructuraPalnEntrega 
    { 
        public sp_plan_entrega_input Header { get; set; }
        public List<UbicacionProductos> UbicacionesProductos { get; set; }
    }

    public class EstructuraPalnEntregaCumplio
    {
        public sp_plan_entrega_input Header { get; set; }
        public List<UbicacionProductos> UbicacionesProductos { get; set; }
        public int cumplio { get; set; }
    }

    public class EstructuraPalnEntregaCumplio_ejec
    {
        public sp_plan_entrega_input_ejec Header { get; set; }
        public List<UbicacionProductos> UbicacionesProductos { get; set; }
        public int cumplio { get; set; }
        public String token { get; set; }

        public String file_name { get; set; }

    }

    public class sp_plan_entrega_input
	{
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public Guid p_tbl_contrato_servidor_resp_id { get; set; }
        public String p_identificador { get; set; }
        public String p_periodo { get; set; }
        public String p_descripcion { get; set; }
        public DateTime p_fecha_ejecucion { get; set;  }
        public String p_activo { get; set; }
        public String p_tipo_entrega { get; set; }
    }
    public class sp_plan_entrega_input_ejec
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public Guid p_tbl_contrato_servidor_resp_id { get; set; }
        public String p_identificador { get; set; }
        public String p_periodo { get; set; }
        public String p_descripcion { get; set; }
        public DateTime p_fecha_ejecucion { get; set; }
        public String p_activo { get; set; }
        public String p_tipo_entrega { get; set; }
        public Boolean p_cumplio_pe { get; set; }
        public Boolean p_ejecucion { get; set; }
    }
    public class UbicacionProductos {
        public Guid tbl_ubicacion_id { get; set; }
        public Guid EjecutorPorUbicacion { get; set; }
        public List<sp_plan_entrega_producto> productos { get; set; }
        [NotMapped]
        public String Ejecutor_nombre { get; set; }
    }
    public class sp_plan_entrega_producto 
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public Guid p_tbl_contrato_producto_id { get; set; }
        public Guid p_tbl_ubicacion_plan_entrega_id { get; set; }
        public String p_estatus { get; set; }
        public int p_cantidad { get; set; }
        public String p_detalle_actividad { get; set; }
        public String p_tipo { get; set; }
        public Boolean cumplio { get; set; }
        public String p_monto { get; set; }
        public String p_monto_iva { get; set; }
        public String p_total { get; set; }
    }
    public class conteoitems
    { 
        public int conteo { get; set; }
    }
    public class Token_confirmacion 
    {
        public String token { get; set; }
    }

    public class Token_eliminar
    {
        public String token_deleted { get; set; }
    }

    public class File_name
    {
        public String FA { get; set; }
        public String FT { get; set; }
    }
}
