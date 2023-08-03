using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Modelos;

namespace Modelos.Interfaz
{
    public interface crud_tbl_obligacion
    {
 
        Response.ResponseGeneric<List<tbl_obligacion>> Consultar(String ID);
        Response.ResponseGeneric<List<tbl_periodo>> GetPeriodo();




    }
}
