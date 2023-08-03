using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.Verificacion
{
    public class lista_verificacion
    {

            public String id { get; set; }
            public String IdDependencia { get; set; }
            public String PuntoAV { get; set; }
            public DateTime Inclusion { get; set; }
            public bool Estatus { get; set; }
            public byte EstatusV { get; set; }
        
    }
}
