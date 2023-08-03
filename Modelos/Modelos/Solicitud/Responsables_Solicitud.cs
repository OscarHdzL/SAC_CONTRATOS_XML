using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class Responsables_Solicitud
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public Guid p_tbl_rol_usuario_id { get; set; }
        public Guid p_tbl_solicitud_id { get; set; }
        public DateTime p_inclusion { get; set; }
    }
}
