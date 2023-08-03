using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_tbl_contrato_producto
    {
        //Response.Response Guardar(T entidad);
        //    Response.Response Modificar(T entidad);
        //    Response.Response Eliminar(T entidad);
        Response.ResponseGeneric<List<DropDownList>> FillDrop(String dependencia);
    }
}
