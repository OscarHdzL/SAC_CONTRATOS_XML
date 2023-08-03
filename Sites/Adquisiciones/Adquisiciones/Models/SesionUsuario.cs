using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeAdquisiciones.Models
{
    public class SesionUsuario
    {
        public string ID_USUARIO { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string CORREO { get; set; }
        public string PASSWORD { get; set; }
        public string ID_INSTANCIA { get; set; }
        public bool ES_SUPER_USUARIO { get; set; }
        public string ID_DEPENDENCIA { get; set; }
        public string ID_ROL { get; set; }
        public string ID_ROL_USUARIO { get; set; }

    }
}
