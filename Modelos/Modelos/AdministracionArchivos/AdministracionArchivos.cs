using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class AdministracionArchivos
    {
        public Guid id { get; set; }
        public String file_extension { get; set; }
        public String file_author { get; set; }
        public String file_descripcion { get; set; }
        public String file_name { get; set; }
        public String versions_title { get; set; }
        public String versions_description { get; set; }
        public String versions_author { get; set; }
        public String versions_size { get; set; }
        public int minutes { get; set; }
        public String file_tbl_tipo_documento_id { get; set; }
        public String file_tbl_contrato_id { get; set; }
    }
    public class ResponseFileManager
    { 
        public String RI { get; set; }
    }

    public class ResponseFileManagerInfo
    {
        public String ext { get; set; }
        public String Name { get; set; }
    }
    public class ResponseFileManagerInfoVersions
    {
        public String ext { get; set; }
        public int Consecutive { get; set; }
    }
    public class ResponseFileList 
    {
        public String id { get; set; } 
        public String file_name { get; set; }
        public String file_extension { get; set; }
        public String file_tbl_tipo_documento_id { get; set; }
        public String file_tbl_contrato_id { get; set; }
    }
}
