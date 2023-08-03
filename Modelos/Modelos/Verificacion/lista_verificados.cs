using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.Verificacion
{
    public class lista_verificados
    {
        public String idpregunta { get; set; }
        public String idverificacion { get; set; }
        public String tbl_dependencia_id { get; set; }
        public String pregunta { get; set; }
        public DateTime inclusion { get; set; }
        public String tbl_estatus_verificacion_id { get; set; }
        public DateTime? fecha_verificacion { get; set; }
        public String pregunta_personalizada { get; set; }

    }

    public class lista_verificion_x_contrato
    {
        public String id { get; set; }
        public String nombre { get; set; }
        public String tbl_dependencia_id { get; set; }
        public String tbl_verificacion_contrato_id { get; set; }
        public DateTime? inclusion { get; set; }
        public DateTime? fecha_verificacion { get; set; }
        public String pregunta_personalizada { get; set; }
        public String tbl_pregunta_formulario_id { get; set; }
        public String pregunta { get; set; }
        public String estatus { get; set; }
    }

}
