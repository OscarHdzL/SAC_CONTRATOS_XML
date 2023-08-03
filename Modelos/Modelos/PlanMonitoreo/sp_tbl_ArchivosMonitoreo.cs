using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class sp_tbl_ArchivosMonitoreo
    {
        public Guid id_ { get; set; }
        public Guid tbl_plan_moniotoreo_id_ { get; set; }
        public Guid tbl_Ubicacion_id_ { get; set; }
        public Guid tbl_obligacion_id_ { get; set; }
        public String token_ { get; set; }
    }
}
