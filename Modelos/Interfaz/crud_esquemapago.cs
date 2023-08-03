using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.EsquemaPago;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_esquemapago
    {
        //Response.Response Guardar(T entidad);
        //    Response.Response Modificar(T entidad);
        //    Response.Response Eliminar(T entidad);
        Response.ResponseGeneric<List<tbl_esquema_pago_new>> ConsultarEsquemasPago(String contrato);


    }
}
