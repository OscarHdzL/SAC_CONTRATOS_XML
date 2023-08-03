using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.ResponsablesApego;
using Modelos.Modelos.ServidoresPublicos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_responsable_apego_contrato_negocio : crud_responsablesapegocontrato
    {
        private tbl_responsable_apego_contrato_acceso_datos _responsablesapego = new tbl_responsable_apego_contrato_acceso_datos();
        private tbl_servidorespublicos_acceso_datos _ServidoresPublicos = new tbl_servidorespublicos_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_responsable_apego_contrato_negocio()
        {
            _logger = new LoggerManager();
        }
        //private tbl_tiposcontrato_acceso_datos _TiposContratos = new tbl_tiposcontrato_acceso_datos();

        public ResponseGeneric<List<DropDownList>> FillDrop(string contrato)
        {
            try
            {
                return _responsablesapego.FillDrop(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_responsable_apego_contrato_regsoldoc>> ConsultarResposables_RegSolDoc(string contrato)
        {
            try
            {
                return _responsablesapego.ConsultarResposables_RegSolDoc(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<tbl_responsable_apego_contrato_regsoldoc>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_servidor_publico>> ConsultarByRol(String Dependencia)
        {
            try
            {

                return _ServidoresPublicos.ConsultarByRol(Dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<tbl_servidor_publico>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_responsable_apego_contrato_responsabilidad>> ConsultarResponsables_Responsabilidades(string contrato)
        {
            try
            {
                return _responsablesapego.ConsultarResponsables_Responsabilidades(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<tbl_responsable_apego_contrato_responsabilidad>>(ex);
            }
        }

        
        public ResponseGeneric<tbl_responsable_apego_contrato_responsabilidad> ConsultarResponsablesById(string contrato, string responsable)
        {
            try
            {
                return _responsablesapego.ConsultarResponsablesById(contrato, responsable);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<tbl_responsable_apego_contrato_responsabilidad>(ex);
            }
        }


        public ResponseGeneric<tbl_contrato_servidor_resp_esquemapago> ConsultarResposable_EsquemaPago(string contrato)
        {
            try
            {
                return _responsablesapego.ConsultarResposable_EsquemaPago(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<tbl_contrato_servidor_resp_esquemapago>(ex);
            }
        }


        public ResponseGeneric<List<Crudresponse>> add(tbl_contrato_servidor_resp_add responsable)
        {
            try
            {
                responsable.p_id = Guid.NewGuid().ToString();
                responsable.p_opt = 2;
                responsable.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                responsable.p_estatus = 1;
                return _responsablesapego.add(responsable);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> update(tbl_contrato_servidor_resp_add responsable)
        {
            try
            {
                responsable.p_opt = 3;
                responsable.p_estatus = 1;
                responsable.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                return _responsablesapego.update(responsable);
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> delete(tbl_contrato_servidor_resp_add responsable)
        {
            try
            {
                responsable.p_opt = 4;
                responsable.p_estatus = 0;
                responsable.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                return _responsablesapego.delete(responsable);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
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
