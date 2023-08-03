using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_solicitud_observador
    {
        public string id { get; set; }
        public string tbl_solicitud_id { get; set; }
        public string tipo_acta { get; set; }
        public string observador { get; set; }
        public string tbl_programacion_id { get; set; }
        public DateTime inclusion { get; set; }
        
    }   
    
    public class tbl_solicitud_observador_list
    {
        public string id { get; set; }
        public string tbl_solicitud_id { get; set; }
        public string tipo_acta { get; set; }
        public string observador { get; set; }
        public string num_solicitud { get; set; }
        public string FolioConvocatoria { get; set; }


    }
}
