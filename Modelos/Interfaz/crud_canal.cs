using Modelos.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_canal
    {
       Response.ResponseGeneric<List<tbl_canal>> Consultar();
    }
}
