using Modelos.Modelos.Responsabilidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_responsabilidades
    {
        //Response.Response Guardar(T entidad);
        //    Response.Response Modificar(T entidad);
        //    Response.Response Eliminar(T entidad);
        Response.ResponseGeneric<List<tbl_responsabilidad>> Consultar();
        //Response.ResponseGeneric<tbl_responsabilidad> ConsultarResponsabilidad();
    }
}
