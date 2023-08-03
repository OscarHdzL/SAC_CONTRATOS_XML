using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Modelos.Contrato;

namespace Modelos.Modelos
{
    public class lista_tipo_interlocutor { 
        public string id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool comercial { get; set; }
        public bool activo { get; set; }
    }
}
