using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Responsabilidades;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_responsabilidades_negocio : crud_responsabilidades 
    { 

        private tbl_responsabilidades_acceso_datos _Responsabilidades = new tbl_responsabilidades_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_responsabilidades_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_responsabilidad>> Consultar()
        {
            try
            {
                return _Responsabilidades.Consultar();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_responsabilidad>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Update_Email(Guid idPersona, String Correo)
        {
            try
            {
                return _Responsabilidades.Update_Email(idPersona, Correo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Update_Email", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        //public ResponseGeneric<tbl_responsabilidad> ConsultarResponsabilidad(String idresponsabilidad, String contrato)
        //{
        //    try
        //    {
        //        return _Responsabilidades.ConsultarResponsabilidad(idresponsabilidad, contrato);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new ResponseGeneric<tbl_responsabilidad>(ex);
        //    }
        //}
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
