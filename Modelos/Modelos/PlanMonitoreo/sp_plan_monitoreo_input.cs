using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class sp_plan_monitoreo_struc
    {
        public sp_plan_monitoreo_input _plan_monitoreo { get; set; }
        public List<sp_plan_monitoreo_ubicacion> ubicaciones { get; set; }
    }

    public class sp_plan_monitoreo_input
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public Guid p_tbl_plan_entrega_id { get; set; }
        public Guid p_tbl_plan_monitoreo_estado_id { get; set; }
        public Guid p_tbl_contrato_servidor_resp_id { get; set; }
        public String p_periodo { get; set; }
        public String p_ejecucion { get; set; }
        public String p_inclusion { get; set; }
        public byte p_activo { get; set; }

    }


    public class sp_plan_monitoreo_ubicacion
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public Guid p_tbl_plan_monitoreo_id { get; set; }
        public Guid p_tbl_ubicacion_plan_entrega_id { get; set; }
        

    }
}
