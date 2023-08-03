using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{ 
    public class comentarios
    {
        public Guid tbl_solicitud_id { get; set; }
        public String num_solicitud { get; set; }
        public String nombre_usuario { get; set; }
        public String comentario { get; set; }
        public DateTime inclusion { get; set; }
        public String fase { get; set; }
    }
}
