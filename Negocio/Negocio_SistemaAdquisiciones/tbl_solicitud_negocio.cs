
using AccesoDatos_SistemaAdquisiciones;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.ServidoresPublicos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;


namespace Negocio_SistemaAdquisiciones
{
    public class tbl_solicitud_negocio
    {
        private sp_solicitud  _solicitud = new sp_solicitud();
        private responsablessolicitud_negocio _responsable = new responsablessolicitud_negocio();
        private tbl_partida_solicitud_contrato_temp_negocio _partidasTemp = new tbl_partida_solicitud_contrato_temp_negocio();
        private tbl_contrato_area_negocio _contratoAreaTemp = new tbl_contrato_area_negocio();
        private tbl_partida_solicitud_contrato_temp_acceso_datos _tbl_partida_solicitud_contrato_temp_acceso_datos = new tbl_partida_solicitud_contrato_temp_acceso_datos();


        public ResponseGeneric<List<CrudresponseIdentificador>> Guardar(sp_solicitud_en solicitud, List<Responsables_Solicitud> ResponsablesForm, List<tbl_partida_solicitud_contrato_temp_add> partidasTemp, List<tbl_contrato_area_add> contratoAreaTemp)
        {
            try
            {
                //solicitud.p_id = Guid.NewGuid();
                
                ResponseGeneric<List<CrudresponseIdentificador>> Step1 = _solicitud.Guardar(solicitud);
                if (Step1.Response[0].id == Guid.Empty) {
                    return new ResponseGeneric<List<CrudresponseIdentificador>>("No se completo Step 1");
                }

                Guid IdSolicitud = Step1.Response[0].id;
                List<CrudresponseIdentificador> lista = new List<CrudresponseIdentificador>();

                lista.Add(Step1.Response[0]);

                foreach (Responsables_Solicitud item in ResponsablesForm) {

                    Guid idResp = Guid.NewGuid();
                    item.p_opt = 2;
                    item.p_id = idResp;
                    item.p_tbl_solicitud_id = IdSolicitud;
                    item.p_inclusion = DateTime.Now;

                    ResponseGeneric<List<Crudresponse>> Step2 = _responsable.Guardar(item);

                    if (Step2.Response[0].cod != "success")
                    {
                        CrudresponseIdentificador obj2 = new CrudresponseIdentificador();
                        obj2.cod = "";
                        obj2.msg = "No se completo Step 2";
                        obj2.id = Guid.Empty;

                        lista.Add(obj2);
                        return new ResponseGeneric<List<CrudresponseIdentificador>>(lista);
                    }

                    CrudresponseIdentificador obj = new CrudresponseIdentificador();

                    obj.cod = Step2.Response[0].cod;
                    obj.msg = Step2.Response[0].msg;
                    //                    obj.id = idResp;
                    obj.id = Guid.Empty;

                    lista.Add(obj);
                }
                //Se comenta hasta que se finalice lo de presupuestos en core
                //foreach (tbl_partida_solicitud_contrato_temp_add item in partidasTemp)
                //{

                //    Guid idPartida = Guid.NewGuid();
                //    item.p_opt = 2;
                //    item.p_id = idPartida;
                //    item.p_id_propietario = IdSolicitud;


                //    ResponseGeneric<List<Crudresponse>> Step3 = _partidasTemp.Guardar(item);

                //    if (Step3.Response[0].cod != "success")
                //    {
                //        CrudresponseIdentificador obj2 = new CrudresponseIdentificador();
                //        obj2.cod = "";
                //        obj2.msg = "No se completo Step 3";
                //        obj2.id = Guid.Empty;

                //        lista.Add(obj2);
                //        return new ResponseGeneric<List<CrudresponseIdentificador>>(lista);
                //    }

                //    CrudresponseIdentificador obj = new CrudresponseIdentificador();

                //    obj.cod = Step3.Response[0].cod;
                //    obj.msg = Step3.Response[0].msg;
                //    obj.id = Guid.Empty;
                //    //obj.id = idPartida;

                //    lista.Add(obj);
                //}

                //foreach (tbl_contrato_area_add item in contratoAreaTemp)
                //{

                //    Guid idPContratoArea = Guid.NewGuid();
                //    item.p_opt = 2;
                //    item.p_id = idPContratoArea;
                //    item.p_tbl_contrato_id = IdSolicitud;
                    

                //    ResponseGeneric<List<Crudresponse>> Step4 = _contratoAreaTemp.Guardar(item);
                    
                //    if (Step4.Response[0].cod != "success")
                //    {
                //        CrudresponseIdentificador obj2 = new CrudresponseIdentificador();
                //        obj2.cod = "";
                //        obj2.msg = "No se completo Step 4";
                //        obj2.id = Guid.Empty;

                //        lista.Add(obj2);
                //        return new ResponseGeneric<List<CrudresponseIdentificador>>(lista);
                //    }

                //    CrudresponseIdentificador obj = new CrudresponseIdentificador();

                //    obj.cod = Step4.Response[0].cod;
                //    obj.msg = Step4.Response[0].msg;
                //    obj.id = Guid.Empty;
                //    //obj.id = idPContratoArea;

                //    lista.Add(obj);
                //}


                return new ResponseGeneric<List<CrudresponseIdentificador>>(lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Update(sp_solicitud_en solicitud)
        {
            try
            {
                solicitud.p_opt = 3;
                return  _solicitud.Update(solicitud);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_solicitud>> getSolicitudes_rolusuario_estatus(String rol_usuario, String sigla_estatus)
        {
            try
            {
                return _solicitud.getSolicitudes_rolusuario_estatus(rol_usuario, sigla_estatus);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<tbl_solicitud>>(ex);
            }
        }


        

        public ResponseGeneric<contador_solicitud> get_contador_Solicitudes_rolusuario_estatus(String rol_usuario, String sigla_estatus)
        {
            try
            {
                return _solicitud.get_contador_Solicitudes_rolusuario_estatus(rol_usuario, sigla_estatus);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<contador_solicitud>(ex);
            }
        }


        public ResponseGeneric<tbl_solicitud> getSolicitud(String id_solicitud)
        {
            try
            {
                return _solicitud.getSolicitud(id_solicitud);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<tbl_solicitud>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> GetFuente_financiamiento(string id_dep)
        {
            try
            {
                return _solicitud.GetFuente_financiamiento(id_dep);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> Get_area_turnar(string id_dep)
        {
            try
            {
                return _solicitud.Get_area_turnar(id_dep);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_tipo_documento>> Get_lista_tipo_documento(String id_instancia)
        {
            try
            {
                return _solicitud.Get_lista_tipo_documento(id_instancia);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<tbl_tipo_documento>> (ex);
            }
        }
        public ResponseGeneric<List<tbl_documento_adjunto_solicitud>> Get_docts_Solicitud(String id_solicitud)
        {
            try
            {
                return _solicitud.Get_docts_Solicitud(id_solicitud);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<tbl_documento_adjunto_solicitud>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_tipo_dictamen>> Get_tipo_dictamen(String id_dependencia)
        {
            try
            {
                return _solicitud.Get_tipo_dictamen(id_dependencia);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<tbl_tipo_dictamen>>(ex);
            }
        }
        public ResponseGeneric<tbl_solicitud_suficiencia> Get_Solicitud_suficiencia_det(String id_solicitud)
        {
            try
            {
                return _solicitud.Get_Solicitud_suficiencia_det(id_solicitud);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<tbl_solicitud_suficiencia>(ex);
            }
        }

        public ResponseGeneric<tbl_solicitud_estudio_mercado> Get_Solicitud_Est_Merc(String id_solicitud)
        {
            try
            {
                return _solicitud.Get_Solicitud_Est_Merc(id_solicitud);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<tbl_solicitud_estudio_mercado>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Update_sol(sp_solicitud_en _tbl_solicitud)
        {
            try
            {
                return _tbl_partida_solicitud_contrato_temp_acceso_datos.Update_sol(_tbl_solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> AddDocumentoAdjunto(tbl_solicitud_documento_adjunto _tbl_solicitud_documento_adjunto)
        {
            try
            {
                if (_tbl_solicitud_documento_adjunto.id == null || _tbl_solicitud_documento_adjunto.id == "")
                {
                    _tbl_solicitud_documento_adjunto.id = Guid.NewGuid().ToString();
                }
                return _solicitud.AddDocumentoAdjunto(_tbl_solicitud_documento_adjunto);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_dcto_adj(string id_docto)
        {
            try
            {
                return _solicitud.Delete_dcto_adj(id_docto);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> add_suficiencia(tbl_suficiencia_add entidad)
        {
            try
            {
                if (entidad.p_id == null || entidad.p_id == "")
                {
                    entidad.p_id = Guid.NewGuid().ToString();
                }
                if (entidad.p_tbl_fuente_financiamiento_id == null)
                {
                    entidad.p_tbl_fuente_financiamiento_id = "";
                }
                return _solicitud.add_suficiencia(entidad);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> add_estudio_mercado(tbl_estudio_mercado entidad)
        {
            try
            {
                if (entidad.id == null || entidad.id == "")
                {
                    entidad.id = Guid.NewGuid().ToString();
                }
                return _solicitud.add_estudio_mercado(entidad);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> add_dictamen_solicitud(tbl_dictamen entidad)
        {
            try
            {
                if (entidad.id == null || entidad.id == "")
                {
                    entidad.id = Guid.NewGuid().ToString();
                }
                if (entidad.tbl_tipo_dictamen_id == null)
                {
                    entidad.tbl_tipo_dictamen_id = "";
                }
                return _solicitud.add_dictamen_solicitud(entidad);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> update_sol_metodo(string parametro, string id_sol, string variable)
        {
            try
            {
                return _solicitud.update_sol_metodo(parametro, id_sol, variable);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> get_partidas_montos_area_unitario(string Dep, string area, string cap)
        {
            try
            {

                return _solicitud.get_partidas_montos_area_unitario( Dep,  area, cap);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
    }
}
