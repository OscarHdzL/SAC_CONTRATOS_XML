using Modelos.Modelos;
using Modelos.Modelos.ServidoresPublicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_servidorespublicos
    {
        //Response.Response Guardar(T entidad);
        //    Response.Response Modificar(T entidad);
        //    Response.Response Eliminar(T entidad);
        Response.ResponseGeneric<List<tbl_servidor_publico>> Consultar(String dependencia);
        Response.ResponseGeneric<List<DropDownList>> FillDrop(String dependencia);
        Response.ResponseGeneric<List<tbl_servidor_publico>> ConsultarByRolEjecutorPEntrega(string Dependencia);
        Response.ResponseGeneric<List<tbl_servidor_publico>> ConsultarByRolEjecutorPMonitoreo(string Dependencia);
        Response.ResponseGeneric<tbl_servidor_publico> ConsultarServidor(string idServidor);
    }
}
