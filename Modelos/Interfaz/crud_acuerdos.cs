using Modelos.Modelos;

using Modelos.Modelos.Contrato;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_acuerdos
    {
        //Response.Response Guardar(T entidad);
        //    Response.Response Modificar(T entidad);
        //    Response.Response Eliminar(T entidad);
        Response.ResponseGeneric<List<tbl_acuerdos_lista>> Consultar(String contrato);
        Response.ResponseGeneric<tbl_acuerdos_lista> ConsultarAcuerdo(String contrato);
        Response.ResponseGeneric<List<Crudresponse>> add(tbl_acuerdo_add contrato);
        Response.ResponseGeneric<List<Crudresponse>> update(tbl_acuerdo_add contrato);
        Response.ResponseGeneric<List<DropDownList>> ConsultarTipos();






    }
}
