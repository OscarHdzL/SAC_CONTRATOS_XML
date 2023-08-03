using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class ModalidadSolParcial
    {
        public String num_solicitud { get; set; }
        public String tipo_solicitud { get; set; }
        public String tipo_contrato_solicitud { get; set; }
        public DateTime fecha_solicitud { get; set; }
        public String elaboro { get; set; }
        public String dependencia { get; set; }
        public String area { get; set; }
        public String proyecto { get; set; }
        public String descripcion { get; set; }
        public float monto_solicitud { get; set; }
        public float monto_autorizado { get; set; }
        public String comentarios { get; set; }
        public String token_solicitante { get; set; }
    }
}
