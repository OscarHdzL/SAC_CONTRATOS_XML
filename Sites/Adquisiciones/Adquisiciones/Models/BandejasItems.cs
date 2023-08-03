using System;
using System.Collections.Generic;
using System.Text;
using ModeloItemBandeja = SistemaDeAdquisiciones.Models.elementos_bandeja;
using ModeloSolicitud = SistemaDeAdquisiciones.Models.tbl_solicitud;

namespace SistemaDeAdquisiciones.Models
{
    public class BandejasItems
    {
        public String Accion { get; set; }
        public String Name { get; set; }

        public Guid Usuario { get; set; }

        public ModeloItemBandeja EntidadBandeja { get; set; }
        
        public List<ModeloSolicitud> Solicitudes { get; set; }

    }
}
