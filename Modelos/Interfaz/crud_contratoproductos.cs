using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.ResponsablesApego;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_contratoproductos
    {
        //    Response.Response Guardar(T entidad);
        //    Response.Response Modificar(T entidad);
        //    Response.Response Eliminar(T entidad);
        //    Response.ResponseGeneric<List<tbl_>> Consultar(String dependencia);
        Response.ResponseGeneric<List<DropDownList>> FillDrop(String contrato);
        
        
    }
}
