using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.Contrato
{
    public class tbl_contrato_list
    {
        public string id { get; set; }
        public string tbl_tipo_contrato_id { get; set; }
        public string numero { get; set; }
        public DateTime fecha_Iinicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public string nombre { get; set; }
        public string tipo_contrato { get; set; }
        public string responsableapegocontractual { get; set; }
        public string tbl_proyecto_id { get; set; }
        public bool? estatus { get; set; }

    }
}
