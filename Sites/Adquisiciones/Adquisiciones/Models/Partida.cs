using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeAdquisiciones.Models
{
    public class Partida
    {
        public string NumContrato { get; set; }
        public string NumPartida { get; set; }
        public string DescPartida { get; set; }
        public string Monto { get; set; }
        public string IdArea { get; set; }
        public string IdPartida { get; set; }
        public string Ejercicio { get; set; }
    }
}
