using Modelos.Modelos.Contrato;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_contrato
    {
        //Response.Response Guardar(T entidad);
        //    Response.Response Modificar(T entidad);
        //    Response.Response Eliminar(T entidad);
        Response.ResponseGeneric<List<tbl_contrato>> Consultar(String dependencia);
        Response.ResponseGeneric<List<tbl_contrato_list>> ConsultarLista(String dependencia);

        Response.ResponseGeneric<List<tbl_contrato_vista>> ConsultarVista(String contrato);
        //Response.ResponseGeneric<List<tbl_tipo_contrato>> ConsultarTiposContrato();
    }
}
