using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_sanciones_negocio : crud_sanciones<tbl_sanciones>
    {
        private tbl_sanciones_acceso_datos _Sanciones = new tbl_sanciones_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_sanciones_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_sanciones>> Consultar()
        {
            try
            {
                return _Sanciones.Consultar();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_sanciones>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_tipo_sancion(tbl_tipo_sancion_add tbl_tipo_sancion_add)
        {
            try
            {
                if (tbl_tipo_sancion_add.p_opt == 2)
                {
                    tbl_tipo_sancion_add.p_id = Guid.NewGuid();
                }
                return _Sanciones.Add_tipo_sancion(tbl_tipo_sancion_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Delete_tipo_sancion(tbl_tipo_sancion_add tbl_tipo_sancion_add)
        {
            try
            {
                return _Sanciones.Delete_tipo_sanciones(tbl_tipo_sancion_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

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
