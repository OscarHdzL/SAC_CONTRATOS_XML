using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_convocatoria_penalizacion
    {
        public int p_opt { get; set; }                     
		public Guid p_id { get; set; }
        public Guid p_tbl_convocatoria_id { get; set; }
        public Guid p_tbl_estatus_obligacion_id { get; set; }
        public String p_penalizacion { get; set; }
        public int p_porcentaje_penalizacion { get; set; }
        public int p_porcentaje_deductiva { get; set; }
        public int p_porcentaje_garantia { get; set; }
        public String p_monto_garantia { get; set; }
    }
    public class tbl_convocatoria_penalizacion_lista
    {
        public Guid id { get; set; }
        public Guid tbl_convocatoria_id { get; set; }
        public String penalizacion { get; set; }
        public int porcentaje_penalizacion { get; set; }
        public int porcentaje_deductiva { get; set; }
        public int porcentaje_garantia { get; set; }
        public Double monto_garantia { get; set; }
        public String folio { get; set; }
    }
}
