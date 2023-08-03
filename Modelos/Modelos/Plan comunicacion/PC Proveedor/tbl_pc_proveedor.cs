using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_pc_proveedor
    {
        public string id { get; set; }
        public string tbl_contrato_id { get; set; }
        public string tbl_proveedor_id { get; set; }
        public string razon_social { get; set; }
        public string telefono { get; set; }
        public string extension { get; set; }
        public string correo_electronico { get; set; }
        //public string tbl_ubicaciones_id { get; set; }
        //public string ubicacion { get; set; }
        public string tbl_tipo_audiencia_id { get; set; }
        public string tbl_tipo_audiencia { get; set; }
        public DateTime inclusion { get; set; }
        public bool activo { get; set; }
    }
}
