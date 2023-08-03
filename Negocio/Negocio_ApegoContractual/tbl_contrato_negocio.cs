using AccesoDatos;
using Modelos.Interfaz;
using Utilidades.Log4Net;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solucion_Negocio
{
    public class tbl_contrato_negocio : crud_contrato
    {
        private tbl_contrato_acceso_datos _Contratos = new tbl_contrato_acceso_datos();
        private readonly ILoggerManager _logger;
        //private tbl_tiposcontrato_acceso_datos _TiposContratos = new tbl_tiposcontrato_acceso_datos();

        public tbl_contrato_negocio()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<tbl_contrato>> Consultar(string dependencia)
        {
            try
            {
                return _Contratos.Consultar(dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_contrato>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_contrato_list>> ConsultarLista(string dependencia)
        {
            try
            {
                return _Contratos.ConsultarLista(dependencia);

            }

            catch (Exception ex)
            {
                _logger.LogError("ConsultarLista", ex);
                return new ResponseGeneric<List<tbl_contrato_list>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_contrato_list>> ConsultarListaXRol(string rol, string dependencia, string usuario)
        {
            try
            {
                return _Contratos.ConsultarListaXRol(rol,dependencia,usuario);

            }

            catch (Exception ex)
            {
                _logger.LogError("ConsultarListaRol", ex);
                return new ResponseGeneric<List<tbl_contrato_list>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_acuerdo_pe>> ConsultarAcuerdosPE(String usuario, String contrato)
        {
            try
            {
                return _Contratos.ConsultarAcuerdosPE(usuario, contrato);

            }

            catch (Exception ex)
            {
                _logger.LogError("ConsultarLista", ex);
                return new ResponseGeneric<List<tbl_acuerdo_pe>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_acuerdo_pe>> ConsultarAcuerdosRC(String contrato)
        {
            try
            {
                return _Contratos.ConsultarAcuerdosRC(contrato);

            }

            catch (Exception ex)
            {
                _logger.LogError("ConsultarLista", ex);
                return new ResponseGeneric<List<tbl_acuerdo_pe>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_contrato_vista>> ConsultarVista(string contrato)
        {
            try
            {
                return _Contratos.ConsultarVista(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarVista", ex);
                return new ResponseGeneric<List<tbl_contrato_vista>>(ex);
            }
        }




        public ResponseGeneric<List<Crudresponse>> CargaMasivaContrato(List<tbl_contrato_add> contrato)
        {

            int Cout = 0;
            List<Crudresponse> ResponseSuccess = new List<Crudresponse>();
            
            foreach (tbl_contrato_add Contrato in contrato)
            {
                Crudresponse mensaje = new Crudresponse();
                Cout++;
                try
                {
                    ResponseGeneric<List<Crudresponse>> Step1 = _Contratos.CargaMasivaContrato(Contrato);
                    if (Step1.Response[0].cod == "success")
                    {
                        mensaje.cod = "success";
                        mensaje.msg = "se agrego un nuevo contrato" + " renglón " + Cout;
                    }
                }
                catch (Exception ex)
                {
                    mensaje.cod = "error";
                    mensaje.msg = "error en el renglón"+ " " + Cout + " inspeccione la línea ";
                    _logger.LogError("CargaMasivaContrato", ex);
                }
                
                ResponseSuccess.Add(mensaje);
            }
            return new ResponseGeneric<List<Crudresponse>>(ResponseSuccess);
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
