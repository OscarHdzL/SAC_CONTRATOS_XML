using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class tbl_tipo_interlocutor
    {
        public Guid id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool comercial { get; set; }
        public bool activo { get; set; }
    }
    public class tbl_tipo_interlocutor_add
    {
        public int p_opt { get; set; }
        public Guid id { get; set; }
        public string nombre { get; set; }
        public int? comercial { get; set; }
    }
}
