using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.ServidoresPublicos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_servidorespublicos_negocio : crud_servidorespublicos
    {
        private tbl_servidorespublicos_acceso_datos _ServidoresPublicos = new tbl_servidorespublicos_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_servidorespublicos_negocio()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<DropDownList>> FillDrop(String dependencia)
        {
            try
            {
                List<DropDownList> drop = new List<DropDownList>();
                ResponseGeneric<List<tbl_servidor_publico>> lista = _ServidoresPublicos.Consultar(dependencia);

                foreach (var item in lista.Response) {
                    DropDownList obj = new DropDownList();
                    obj.Value = item.id;
                    obj.Text = item.nombre + " " + item.ap_paterno + " " + item.ap_materno;
                    drop.Add(obj);
                }

                //return new ResponseGeneric<drop>();
                return new ResponseGeneric<List<DropDownList>>(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_servidor_publico>> Consultar(String entidad)
        {
            try
            {

                return _ServidoresPublicos.Consultar(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_servidor_publico>>(ex);
            }
        }


        public ResponseGeneric<List<sp_get_vs_servidor_publico_contrato>> get_vs_servidor_publico(sp_get_vs_servidor_publico_input entidad)
        {
            try
            {

         
                    return _ServidoresPublicos.get_vs_servidor_publico(entidad);
                
                
           


            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_get_vs_servidor_publico_contrato>>(ex);
            }
        }


        public ResponseGeneric<List<sp_get_vs_servidor_publico_>> get_vs_servidor_publico_dep(sp_get_vs_servidor_publico_input entidad)
        {
            try
            {


                return _ServidoresPublicos.get_vs_servidor_publico_dep(entidad);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_get_vs_servidor_publico_>>(ex);
            }
        }


        public ResponseGeneric<List<sp_get_vs_servidor_publico_contrato_c>> get_vs_servidor_publico_contrato(sp_get_vs_servidor_publico_input entidad)
        {
            try
            {

             
                    return _ServidoresPublicos.get_vs_servidor_publico_por_contrato(entidad);
               
              



            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_get_vs_servidor_publico_contrato_c>>(ex);
            }
        }



        public ResponseGeneric<List<DropDownList>> get_ejecutores_dependencia(Guid entidad)
        {
            try
            {

                return _ServidoresPublicos.get_ejecutores_dependencia(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> get_tbl_estado()
        {
            try
            {

                return _ServidoresPublicos.get_tbl_estado();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> get_tbl_estado_ciudad(Guid id)
        {
            try
            {

                return _ServidoresPublicos.get_tbl_estado_ciudad(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> get_responsable_pe_contrato(Guid id)
        {
            try
            {

                return _ServidoresPublicos.get_responsable_pe_contrato(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> get_responsablescontrato(Guid entidad)
        {
            try
            {

                return _ServidoresPublicos.get_responsablescontrato(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> get_ResponsablesUbicaciones(Guid entidad)
        {
            try
            {

                return _ServidoresPublicos.get_ResponsablesUbicaciones(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        //get_ResponsablesUbicaciones

        public ResponseGeneric<List<tbl_servidor_publico>> ConsultarByRolEjecutorPEntrega(string Dependencia)
        {
            try
            {
                return _ServidoresPublicos.ConsultarByRolEjecutorPEntrega(Dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_servidor_publico>>(ex);
            }
        }


        public ResponseGeneric<List<tbl_servidor_publico>> ConsultarByRolEjecutorPMonitoreo(string Dependencia)
        {
            try
            {
                return _ServidoresPublicos.ConsultarByRolEjecutorPMonitoreo(Dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_servidor_publico>>(ex);
            }
        }

        public ResponseGeneric<tbl_servidor_publico> ConsultarServidor(string idServidor)
        {
            try
            {
                return _ServidoresPublicos.ConsultarServidor(idServidor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<tbl_servidor_publico>(ex);
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
