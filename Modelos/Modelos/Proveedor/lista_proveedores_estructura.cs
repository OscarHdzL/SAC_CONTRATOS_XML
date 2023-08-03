using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Modelos.Contrato;

namespace Modelos.Modelos
{
    public class lista_proveedores_estructura
    {
        public tbl_proveedor _tbl_proveedor { get; set; }
        public List<tbl_contrato> _tbl_contrato { get; set; }
    }
}
