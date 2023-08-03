using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class CargaMasiva 
    {
        public Guid idAreas { get; set; }
        public String Areas { get; set; }
        public Guid idSubAreas { get; set; }
        public String SubAreas { get; set; }
        public Guid idOficina { get; set; }
        public String Oficina { get; set; }
    }

   public class ErrorLst
    {
        public String Tipo { get; set; }
        public String Estatus { get; set; }
        public String Id { get; set; }
        public String Descripcion { get; set; }
        public ErrorLst(String Tipo_,String Estatus_, String Id_, String Descripcion_) {
            this.Descripcion = Descripcion_;
            this.Estatus = Estatus_;
            this.Id = Id_;
            this.Tipo = Tipo_;
        }
    }

    public class ResponseCMAarea
    {
        public String id { get; set; }
        public String Area { get; set; }
        public String Status { get; set; }

        public ResponseCMAarea(String id_, String Area_, String Estaus_)
        {
            this.id = id_;
            this.Area = Area_;
            this.Status = Estaus_;
        }
    }
    public class ResponseCMSubArea
    {
        public String id { get; set; }
        public String SubArea { get; set; }
        public String Status { get; set; }
        public ResponseCMSubArea(String id_, String SubArea_, String Estaus_)
        {
            this.id = id_;
            this.SubArea = SubArea_;
            this.Status = Estaus_;
        }
    }


    public class CargasMasivas
    {
        public Dictionary<String, String> SPDictionary { get; set; }
    }

    public class DictionaryAreas : CargasMasivas { }

    public class SubAreaMasiva{
        public Guid p_tbl_area_id { get; set; }
        public String p_subarea { get; set; }
    }
    public class SubordinadaMasiva
    {
        public Guid p_tbl_subarea_id { get; set; }
        public String p_area_subordinada { get; set; }
    }
}
