using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministracionContractual
{
    public class dependencia_estructura
    {
        public String id { get; set; }
        public String dependencia { get; set; }
        public int hijos { get; set; }
    }
    public class area_estructura
    {
        public String id { get; set; }
        public String area { get; set; }
        public int hijos { get; set; }
        public String tbl_dependencia_id { get; set; }
    }
    public class subarea_estructura
    {
        public String id { get; set; }
        public String subarea { get; set; }
        public int hijos { get; set; }
        public String tbl_dependencia_id { get; set; }
    }
    public class areasubordinada_estructura
    {
        public String id { get; set; }
        public String area_subordinada { get; set; }
        public String tbl_dependencia_id { get; set; }
    }

    public class estructura_organica_core
    {
        public dependencia_estructura _dependencia { get; set; }
        public List<estructura_area> _areas { get; set; }
    }
    public class estructura_area
    {
        public area_estructura _area { get; set; }
        public List<estructura_subarea> _subareas { get; set; }
    }
    public class estructura_subarea
    {
        public subarea_estructura _subarea { get; set; }
        public List<areasubordinada_estructura> _oficinas { get; set; }
    }
}
