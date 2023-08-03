using Modelos.Modelos;
using Modelos.Modelos.PreguntasFormulario;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_preguntasformulario
    {
        //Response.Response Guardar(T entidad);
        //    Response.Response Modificar(T entidad);
        //    Response.Response Eliminar(T entidad);
        Response.ResponseGeneric<List<tbl_pregunta_formulario>> Consultar(String Dependencia);
        Response.ResponseGeneric<List<Crudresponse>> add(tbl_pregunta_formulario_add pregunta);
    }
}
