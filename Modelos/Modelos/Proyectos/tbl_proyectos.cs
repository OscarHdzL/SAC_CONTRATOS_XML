using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.Proyectos
{
    public class tbl_proyectos
    {
              public string id { get; set; }
              public string tbl_tipo_proyecto_id { get; set; }
              public string proyecto { get; set; }
              public string objetivo { get; set; }
              public string alcance { get; set; }
              public string tbl_criticidad_proyecto_id { get; set; }
              public DateTime fecha_incio { get; set; }
              public DateTime fecha_fin { get; set; }
              public string tbl_estatus_proyecto_id { get; set; }
              public string tbl_tipo_ejecucion_id { get; set; }
              public string tbl_etapa_proyecto_id { get; set; }
              public string tbl_tipo_analisis_id { get; set; }
              public string tbl_nivel_analisis_id { get; set; }
              public int criterio_economico { get; set; }
              public int criterio_social { get; set; }
              public int criterio_ambienta { get; set; }
              public int criterio_politico { get; set; }
              public int criterio_tecnico_institucional { get; set; }
              public string tbl_dependencia_id { get; set; }
       
    }
    public class tbl_lista_proyectos
    {        
        public string id { get; set; }
        public string proyecto { get; set; }
        public DateTime fecha_incio { get; set; }
        public DateTime fecha_fin { get; set; }
        public int tiene_contrato { get; set; }
        public int tiene_documentos { get; set; }
    }
    
}
