
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_agendados
    {
        public Guid ID_EVENTO_PROGRAMADO { get; set; }
        public String Estado { get; set; }
        public String Ciudad { get; set; }
        public DateTime Inclusion { get; set; }
        public String Estatus { get; set; }
        public String Token { get; set; }
    }
}
