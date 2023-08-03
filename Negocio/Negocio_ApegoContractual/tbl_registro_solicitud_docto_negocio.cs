using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.RegSolDoc;
using Modelos.Modelos.ResponsablesApego;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_registro_solicitud_docto_negocio : crud_regsoldoc
    {
        private tbl_registro_solicitud_docto_acceso_datos _regSolDoc = new tbl_registro_solicitud_docto_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_registro_solicitud_docto_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_registro_solicitud_docto_list>> Consultar_RegSolDoc(String contrato)
        {
            try
            {
                return _regSolDoc.Consultar_RegSolDoc(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_registro_solicitud_docto_list>>(ex);
            }
        }

        public ResponseGeneric<tbl_contrato_solicitud_docto> GetSolicitud_RegSolDoc(String contrato, String solicitud)
        {
            try
            {
                return _regSolDoc.GetSolicitud_RegSolDoc(contrato, solicitud);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<tbl_contrato_solicitud_docto>(ex);
            }
        }


        public ResponseGeneric<List<tbl_contrato_solicitud_docto_expediente>> GetSolicitud_Expediente(String contrato)
        {
            try
            {
                return _regSolDoc.GetSolicitud_Expediente(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<tbl_contrato_solicitud_docto_expediente>>(ex);
            }
        }

        


        public ResponseGeneric<List<Crudresponse>> add(tbl_contrato_solicitud_docto_add regsoldoc,int p_opt)
        {
            try
            {
                if (regsoldoc.p_id == Guid.Empty.ToString())
                {
                    regsoldoc.p_id = Guid.NewGuid().ToString();
                }
                regsoldoc.p_opt = p_opt;
                regsoldoc.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                regsoldoc.p_estatus = 1;
                return _regSolDoc.add(regsoldoc);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
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
