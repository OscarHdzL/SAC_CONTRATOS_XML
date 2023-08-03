using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_fallo
    {        
        public string id { get; set; }
        public string rfc { get; set; }
        public string nombre { get; set; }
        public string licitante { get; set; }
        public bool ganador { get; set; }
        
    }
    public class tbl_firmantes
    {
        public string firmantes { get; set; }
    }
    public class tbl_Responsable
    {
        public string Responsable { get; set; }
    }
    public class tbl_Proveedores
    {
        public string id_proveedor { get; set; }
    }
    public class json_presupuesto_sol 
    {
        public String json_pres { get; set; }
    }
}
