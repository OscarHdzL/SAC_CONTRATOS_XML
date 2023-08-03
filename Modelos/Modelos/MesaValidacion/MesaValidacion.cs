using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class SolicitudMesaValidacion
    {
        public Guid id { get; set; }
        public Guid solicitud { get; set; }
        public DateTime inclusion { get; set; }
        public String Token { get; set; }
    }
}
