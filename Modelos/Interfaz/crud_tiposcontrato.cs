using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_tiposcontrato<T>
    {
        //Response.Response Guardar(T entidad);
        //    Response.Response Modificar(T entidad);
        //    Response.Response Eliminar(T entidad);
        Response.ResponseGeneric<List<T>> Consultar();
    }
}
