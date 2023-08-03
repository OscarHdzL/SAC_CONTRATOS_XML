using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.PreguntasFormulario;
using Modelos.Modelos.Verificacion;
using Modelos.Modelos.VerificacionContrato;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_verificacion_contrato_negocio : crud_verificacioncontrato
    {
        private tbl_verificacion_contrato_acceso_datos _Verificaciones = new tbl_verificacion_contrato_acceso_datos();
        private readonly ILoggerManager _logger;
        private tbl_preguntas_formulario_acceso_datos _Preguntas = new tbl_preguntas_formulario_acceso_datos();

        public tbl_verificacion_contrato_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<lista_verificados>> Consultar(string Dependencia, string Contrato)
        {
            try
            {
                List<lista_verificados> listaV = new List<lista_verificados>();

                List<tbl_pregunta_formulario> ListaPreguntas = _Preguntas.Consultar(Dependencia).Response;


                foreach (tbl_pregunta_formulario preg in ListaPreguntas) {

                    tbl_verificacion_contrato objverificacion = _Verificaciones.ConsultarVerficacionPregunta(Contrato, preg.id).Response;


                    if (objverificacion != null)
                    {
                        listaV.Add(new lista_verificados()
                        {
                            idpregunta = preg.id,
                            idverificacion = objverificacion.id,
                            tbl_dependencia_id = preg.tbl_dependencia_id,
                            pregunta = preg.pregunta,
                            inclusion = preg.inclusion,
                            tbl_estatus_verificacion_id = objverificacion.tbl_estatus_verificacion_id,
                            fecha_verificacion = objverificacion.fecha_verificacion,
                            pregunta_personalizada = objverificacion.pregunta_personalizada
                        }
                        );
                    }
                    else {
                        listaV.Add(new lista_verificados()
                        {
                            idpregunta = preg.id,
                            idverificacion = Guid.Empty.ToString(),
                            tbl_dependencia_id = preg.tbl_dependencia_id,
                            pregunta = preg.pregunta,
                            inclusion = preg.inclusion,
                            tbl_estatus_verificacion_id = "0", //Id correspondiente a NO
                            fecha_verificacion = null,
                            pregunta_personalizada = null
                        }
                        );
                    }
                }

                return new ResponseGeneric<List<lista_verificados>>(listaV);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_verificados>>(ex);
            }
        }

        public ResponseGeneric<List<lista_verificados>> Consultar_SinContrato(string Dependencia)
        {
            try
            {
                List<lista_verificados> listaV = new List<lista_verificados>();

                List<tbl_pregunta_formulario> ListaPreguntas = _Preguntas.Consultar(Dependencia).Response;

                foreach (tbl_pregunta_formulario preg in ListaPreguntas)
                {
                        listaV.Add(new lista_verificados()
                        {
                            idpregunta = preg.id,
                            idverificacion = Guid.Empty.ToString(),
                            tbl_dependencia_id = preg.tbl_dependencia_id,
                            pregunta = preg.pregunta,
                            inclusion = preg.inclusion,
                            tbl_estatus_verificacion_id = "0" //Id correspondiente a NO
                        }
                        );

                }

                return new ResponseGeneric<List<lista_verificados>>(listaV);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_verificados>>(ex);
            }
        }

        public ResponseGeneric<tbl_verificacion_contrato> ConsultarVerficacionPregunta(string Contrato, string Pregunta)
        {
            throw new NotImplementedException();
        }


        public ResponseGeneric<List<Crudresponse>> add(tbl_verificacion_contrato_add Verificacion)
        {
            try
            {
                if (Verificacion.p_id == null || Verificacion.p_id == Guid.Empty.ToString())
                {
                    Verificacion.p_id = Guid.NewGuid().ToString();
                    Verificacion.p_opt = 2;
                }
                else { //Actualiza por que ya existe un id
                    Verificacion.p_opt = 3;
                }
                Verificacion.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                return _Verificaciones.add(Verificacion);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> addV2(tbl_verificacion_contrato_add Verificacion)
        {
            try
            {
                if (Verificacion.p_id == null || Verificacion.p_id == Guid.Empty.ToString())
                {
                    Verificacion.p_id = Guid.NewGuid().ToString();
                    Verificacion.p_opt = 2;
                }
                else
                { //Actualiza por que ya existe un id
                    Verificacion.p_opt = 3;
                }
                Verificacion.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                return _Verificaciones.addV2(Verificacion);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<lista_verificion_x_contrato>> GetVerificacionxContrato(String contrato)
        {
            try
            {
                return _Verificaciones.GetVerificacionxContrato(contrato);
            }
            catch (Exception ex) {
                _logger.LogError("GetVerificacionxContrato", ex);
                return new ResponseGeneric<List<lista_verificion_x_contrato>>(ex);
            }
        }



        public ResponseGeneric<List<Crudresponse>> delete(tbl_verificacion_contrato_add Verificacion)
        {
            try
            {
               
                    Verificacion.p_opt = 4;
               
                Verificacion.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                return _Verificaciones.delete(Verificacion);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> deleteV2(tbl_verificacion_contrato_add Verificacion)
        {
            try
            {

                Verificacion.p_opt = 4;
                Verificacion.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                return _Verificaciones.DeleteV2(Verificacion);
            }
            catch (Exception ex)
            {
                _logger.LogError("deleteV2", ex);
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
