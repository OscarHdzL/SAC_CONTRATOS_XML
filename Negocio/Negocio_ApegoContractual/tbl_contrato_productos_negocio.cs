using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.ResponsablesApego;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_contrato_productos_negocio : crud_contratoproductos
    {
        private tbl_contrato_productos_acceso_datos _responsablesapego = new tbl_contrato_productos_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_contrato_productos_negocio()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<DropDownList>> FillDrop(string contrato)
        {
            try
            {
                return _responsablesapego.FillDrop(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("FillDrop", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        

        //public Response Eliminar(TBL_SANCIONES entidad)
        //{
        //    try
        //    {
        //        return _Sanciones.Eliminar(entidad);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response(ex);
        //    }
        //}

        //public Response Guardar(TBL_SANCIONES entidad)
        //{
        //    try
        //    {
        //        return _Sanciones.Guardar(entidad);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response(ex);
        //    }
        //}

        //public Response Modificar(TBL_SANCIONES entidad)
        //{
        //    try
        //    {
        //        return _Sanciones.Modificar(entidad);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response(ex);
        //    }
        //}
    }
}
