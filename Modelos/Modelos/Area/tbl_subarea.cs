using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_subarea
    {
        public int p_opt { get; set; }
	    public Guid p_id { get; set; }
        public String p_tbl_area_id { get; set; }
        public String p_subarea { get; set; }
    }

    public class tbl_area_subordinada
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public String p_tbl_subarea_id { get; set; }
        public String p_area_subordinada { get; set; }
    }
}
