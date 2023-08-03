using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos.Area
{
    public class tbl_area
    {
        public string id { get; set; }
        public string tbl_dependencia_id { get; set; }
        public string area { get; set; }
        public int total_personal { get; set; }
        public double sueldo_promedio { get; set; }
        public int total_externos { get; set; }
        public int nivel { get; set; }
        public string id_area_padre { get; set; }
    }
    public class tbl_lista_areas
    {
        public string id { get; set; }
        public string tbl_dependencia_id { get; set; }
        public string area { get; set; }
        public int num_hijas { get; set; }
        public int puestos { get; set; }
        public string id_area_padre { get; set; }

    }

    public class tbl_areas_lista 
    {
        public string id { get; set; }
        public string id_dependencia { get; set; }
        public string area { get; set; }
        public string dependencia { get; set; }
        public int num_sub { get; set; }
    }
    public class tbl_subareas_lista
    {
        public string id { get; set; }
        public string tbl_area_id { get; set; }
        public string subarea { get; set; }
        public string area { get; set; }
        public int num_sub { get; set; }
    }
    public class tbl_areasubordinada_lista
    {
        public string id { get; set; }
        public string tbl_subarea_id { get; set; }
        public string area_subordinada { get; set; }
        public string subarea { get; set; }
    }
}
