using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministracionContractual
{
    public enum ETipoItem
    {
        dependencia = 0,
        area = 1,
        proyecto = 2
    }
    public class estructura_lista_dep_areas
    {
        public String Texto { get; set; }
        public ETipoItem TipoItem { get; set; }
        public Guid? IdItem { get; set; }
        public List<estructura_lista_dep_areas> Hijos { get; set; }
    }
}
