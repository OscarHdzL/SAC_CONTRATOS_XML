using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;

using Modelos.Response;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_ubicacion_negocio : crud_tbl_ubicacion
    {
        private tbl_ubicacion_acceso_datos _ubicacion = new tbl_ubicacion_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_ubicacion_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_ubicacion_output>> Consultar(tbl_ubicacion_input tbl_ubicacion_input_)
        {
            try
            {
                List<tbl_ubicacion_output> List = _ubicacion.Consultar(tbl_ubicacion_input_).Response;
               
                return new ResponseGeneric<List<tbl_ubicacion_output>>(List);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_ubicacion_output>>(ex);
            }
        }



        public ResponseGeneric<List<plan_entrega_ubicacion>> ConsultarPlanEntrega(Guid tbl_plan_entrega_id)
        {
            try
            {
                List<plan_entrega_ubicacion> List = _ubicacion.ConsultarPlanEntrega(tbl_plan_entrega_id).Response;

                return new ResponseGeneric<List<plan_entrega_ubicacion>>(List);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<plan_entrega_ubicacion>>(ex);
            }
        }

        public ResponseGeneric<List<plan_entrega_ubicacion>> ConsultarPlanEntregaUbicaciones(Guid tbl_plan_entrega_id, Guid user)
        {
            try
            {
                List<plan_entrega_ubicacion> List = _ubicacion.get_ubic_pe_group(tbl_plan_entrega_id, user).Response;

                return new ResponseGeneric<List<plan_entrega_ubicacion>>(List);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<plan_entrega_ubicacion>>(ex);
            }
        }


        public ResponseGeneric<List<lista_plan_entrega_ubicacion>> ConsultarPlanEntregaUbicaciones_token(Guid tbl_plan_entrega_id, Guid tbl_usuario_id)
        {
            try
            {
                List<plan_entrega_ubicacion> List = _ubicacion.get_ubic_pe_group(tbl_plan_entrega_id, tbl_usuario_id).Response;
                List<lista_plan_entrega_ubicacion> Lista_Ubicaciones_token = new List<lista_plan_entrega_ubicacion>();
                foreach (plan_entrega_ubicacion item in List)
                {
                    lista_plan_entrega_ubicacion ubicacion_token = new lista_plan_entrega_ubicacion();
                    ubicacion_token.ubicacion = new plan_entrega_ubicacion();
                    ubicacion_token.ubicacion = item;
                    ubicacion_token.token = _ubicacion.sp_get_token_ubicacion(tbl_plan_entrega_id, item.tbl_ubicacion_id).Response[0].token;
                    Lista_Ubicaciones_token.Add(ubicacion_token);
                }

                return new ResponseGeneric<List<lista_plan_entrega_ubicacion>>(Lista_Ubicaciones_token);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_plan_entrega_ubicacion>>(ex);
            }
        }

        public ResponseGeneric<dynamic> DeleteReportePlanEntregaUbicaciones_token(Guid tbl_plan_entrega_id, Guid tbl_ubicacion_id)
        {
            try
            {
                _ubicacion.sp_delete_token_ubicacion(tbl_plan_entrega_id, tbl_ubicacion_id);
                return new ResponseGeneric<dynamic>(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<dynamic>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add(UbicacionEjecutores_add tbl_ubicacion_add)
        {
            try
            {
                ResponseGeneric<List<CrudresponseIdentificador>> Step1 =  _ubicacion.Add(tbl_ubicacion_add.Ubicacion);
                if (Step1.Response[0].id == Guid.Empty)
                { 
                    return new ResponseGeneric<List<Crudresponse>>("No se pudo almacenar la ubicación|step1");
                }
                if (tbl_ubicacion_add.Ubicacion.p_opt == 3)
                {
                    ResponseGeneric<List<Crudresponse>> Step0 = _ubicacion.Addservidor(
                         4,
                         Step1.Response[0].id.ToString(),
                         tbl_ubicacion_add.Ejecutores.p_str_ids
                     );
                }
                ResponseGeneric<List<Crudresponse>> Step2 = _ubicacion.Addservidor(
                        tbl_ubicacion_add.Ubicacion.p_opt == 3 ? 2 : tbl_ubicacion_add.Ubicacion.p_opt, 
                        Step1.Response[0].id.ToString(), 
                        tbl_ubicacion_add.Ejecutores.p_str_ids
                    );
                if (Step2.Response[0].cod != "success")
                {
                    return new ResponseGeneric<List<Crudresponse>>("No se pudo almacenar la ubicación|step2");
                }
                List<Crudresponse> ResponseSuccess = new List<Crudresponse>();
                Crudresponse CR = new Crudresponse();
                CR.cod = "success";
                CR.msg = "Se agrego un nuevo registro";
                ResponseSuccess.Add(CR);
                return new ResponseGeneric<List<Crudresponse>>(ResponseSuccess);

            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        ///SOLO ACTUALIZA o AGREGA
        public ResponseGeneric<Crudresponse> add_update_ubicacion_ejecutor(ubicacion_ejecutor ubicacion_ejecutor)
        {
            try
            {
                if (ubicacion_ejecutor.p_tbl_ubicacion_servidor_id != null && ubicacion_ejecutor.p_tbl_ubicacion_servidor_id != Guid.Empty)
                {
                    ubicacion_ejecutor.p_opt = 3;
                }
                else {
                    ubicacion_ejecutor.p_opt = 2;
                }
                
                
                return _ubicacion.add_update_ubicacion_ejecutor(ubicacion_ejecutor);


            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

        ///SOLO ACTUALIZA
        public ResponseGeneric<Crudresponse> eliminar_ubicacion_ejecutor(ubicacion_ejecutor ubicacion_ejecutor)
        {
            try
            {
                ubicacion_ejecutor.p_opt = 4;

                return _ubicacion.add_update_ubicacion_ejecutor(ubicacion_ejecutor);


            }
            catch (Exception ex)
            {
                _logger.LogError("eliminar", ex);
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

        ///SOLO ACTUALIZA
        public ResponseGeneric<List<CrudresponseIdentificador>> update_ubicacion(tbl_ubicacion_add tbl_ubicacion_add)
        {
            try
            {
                
                tbl_ubicacion_add.p_opt = 3;

                return _ubicacion.Add(tbl_ubicacion_add);


            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        } 
        
        public ResponseGeneric<validar_ubicacion_ligada> validar_ubicacion_ligada(Guid ubicacion)
        {
            try
            {
              
                return _ubicacion.validar_ubicacion_ligada(ubicacion);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<validar_ubicacion_ligada>(ex);
            }
        }

        public ResponseGeneric<List<CrudresponseIdentificador>> Delete(Guid id)
        {
            try
            {
                tbl_ubicacion_add obj = new tbl_ubicacion_add();

                obj.p_opt = 4;
                obj.p_id = id;

                //obj.Ejecutores = new ubicacionEjecutores();
                //obj.Ejecutores.p_opt = 4;
                //obj.Ejecutores.p_str_ids = Guid.Empty.ToString(); 
                //obj.Ejecutores.p_tbl_ubicacion_id = id;



                //ResponseGeneric<List<Crudresponse>> Step1 = _ubicacion.Addservidor(obj.Ejecutores.p_opt, obj.Ejecutores.p_tbl_ubicacion_id.ToString(), obj.Ejecutores.p_str_ids);
                //if (Step1.Response[0].cod != "success")
                //{
                //    return new ResponseGeneric<List<Crudresponse>>("No se pudo eliminar ubicacion|Step 1");
                //}
                return _ubicacion.Add(obj);
                //if (Step2.Response[0].cod != "success")
                //{
                //    return new ResponseGeneric<List<Crudresponse>>("Se eliminaron los ejecutores de ubicacion pero no la ubicacion|Step 2");
                //}
                //List<Crudresponse> ResponseSuccess = new List<Crudresponse>();
                //Crudresponse CR = new Crudresponse();
                //CR.cod = "success";
                //CR.msg = "Se agrego un nuevo registro";
                //ResponseSuccess.Add(CR);
                //return new ResponseGeneric<List<Crudresponse>>(ResponseSuccess); ;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }

        public ResponseGeneric<List<File_name>> _sp_download_filename_ubicacion(string ubicacion, string clave)
        {
            try
            {
                return _ubicacion._sp_download_filename_ubicacion(ubicacion, clave);
            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<File_name>>(ex);
            }
        }

        public ResponseGeneric<dynamic> deleted_file_archivo_ubicacion(string token)
        {
            try
            {
                ResponseGeneric<dynamic> response = _ubicacion.deleted_file_archivo_ubicacion(token);

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<dynamic>(ex);
            }
        }

    }
}
