using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using System;
using Utilidades.Log4Net;
using System.Collections.Generic;
using System.Text;

namespace Solucion_Negocio
{
    public class tbl_areas_negocio : crud_areas
    {
        private tbl_areas_acceso_datos _area = new tbl_areas_acceso_datos();
        private readonly ILoggerManager _logger;
        //private tbl_tiposcontrato_acceso_datos _TiposContratos = new tbl_tiposcontrato_acceso_datos();
        public tbl_areas_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<DropDownList>> FillDrop(string dependencia)
        {
            try
            {
                return _area.FillDrop(dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
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
