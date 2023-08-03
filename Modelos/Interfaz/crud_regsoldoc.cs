using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.RegSolDoc;
using Modelos.Modelos.ResponsablesApego;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_regsoldoc
    {
        //    Response.Response Guardar(T entidad);
        //    Response.Response Modificar(T entidad);
        //    Response.Response Eliminar(T entidad);
        //    Response.ResponseGeneric<List<tbl_>> Consultar(String dependencia);
        
        Response.ResponseGeneric<List<tbl_registro_solicitud_docto_list>> Consultar_RegSolDoc(String contrato);
        Response.ResponseGeneric<tbl_contrato_solicitud_docto> GetSolicitud_RegSolDoc(String contrato, String solicitud);
        //Response.ResponseGeneric<List<tbl_tipo_contrato>> ConsultarTiposContrato();
    }
}
