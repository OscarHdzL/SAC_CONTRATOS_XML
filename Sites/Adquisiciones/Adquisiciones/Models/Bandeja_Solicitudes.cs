using System;
using System.Collections.Generic;
using System.Text;
using ModeloItemBandeja = SistemaDeAdquisiciones.Models.elementos_bandeja;
using ModeloSolicitud = SistemaDeAdquisiciones.Models.tbl_solicitud;
namespace SistemaDeAdquisiciones.Models
{
    public class Bandeja_Solicitudes
    {
        public ModeloItemBandeja Bandeja { get; set; }
        public List<ModeloSolicitud> Solicitudes { get; set; }

      

    }
}
