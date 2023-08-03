using AccesoDatos_SistemaAdquisiciones;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
namespace Negocio_SistemaAdquisiciones
{
    public class tbl_fallo_negocio : crud_fallo
    {
        private tbl_fallo_acceso_datos _acceso_datos = new tbl_fallo_acceso_datos();
        private avanzar_fase_negocio Negocio_avanzar_fase = new avanzar_fase_negocio();
        public ResponseGeneric<List<tbl_fallo>> Proposiciones_Get(string id_sol)
        {
            try
            {
                return _acceso_datos.Proposiciones_Get(id_sol);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_fallo>>(ex);
            }
        }
        public ResponseGeneric<List<sp_config_contrato>> Get_New_Con(string id_sol)
        {
            try
            {
                return _acceso_datos.Get_New_Con(id_sol);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<sp_config_contrato>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_firmantes>> Get_Firm_Sol(string id_sol)
        {
            try
            {
                return _acceso_datos.Get_Firm_Sol(id_sol);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_firmantes>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_Responsable>> Get_Res_Sol(string id_sol)
        {
            try
            {
                return _acceso_datos.Get_Res_Sol(id_sol);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_Responsable>>(ex);
            }
        }
        public ResponseGeneric<tbl_Proveedores> Get_lista_prov(string rfc_licitante)
        {
            try
            {
                return _acceso_datos.Get_lista_prov(rfc_licitante);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<tbl_Proveedores>(ex);
            }
        }
        public ResponseGeneric<List<json_presupuesto_sol>> Get_json_pres_sol(string id_sol)
        {
            try
            {
                return _acceso_datos.Get_Json_Pres_Sol(id_sol);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<json_presupuesto_sol>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Fase_0_ADD(sp_config_contrato contrato)
        {
            try
            {
                contrato.p_id = Guid.NewGuid();
                return _acceso_datos.config(contrato);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public Boolean responsables_contrato(Responsable_contrato rc)
        {
            try
            {
                foreach (String val in rc.firmantes)
                {
                    _acceso_datos.responsables_contrato(Guid.Parse(val), Guid.Parse(rc.Contrato), 0, "insert");
                }
                _acceso_datos.responsables_contrato(Guid.Parse(rc.Responsable), Guid.Parse(rc.Contrato), 1, "insert");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Boolean add_contrato_proveedor(List<string> lst, Guid contrato)
        {
            try
            {
                foreach (string val in lst)
                {
                    _acceso_datos.add_contrato_proveedor(val, contrato, "insert");
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean comprometer_presupuesto_area(List<PresupuestoContrato> obj, Guid Contrato)
        {
            try
            {
                foreach (PresupuestoContrato value in obj)
                {
                    comprometer_presupuesto_area_input Cmp = new comprometer_presupuesto_area_input();
                    Cmp.p_opt = 3;
                    Cmp.p_tbl_contrato_id = Contrato;
                    Cmp.p_tbl_dependencia_id = value.dependencia;
                    Cmp.p_tbl_capitulo_gasto_id = value.idcapgast;
                    Cmp.p_tbl_ejercicio_id = Guid.Parse("6d19ad5e-37ed-11ea-82d7-00155d1b3502");
                    Cmp.p_tbl_area_id = value.areaSeleccionada;
                    Cmp.p_monto_a_comprometer = value.monto_por_ejercer;
                    _acceso_datos.comprometer_presupuesto_area(Cmp);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ResponseGeneric<Crudresponse> GenerarFallo(Guid id_sol, String LstPres)
        {
            try
            {

                List<tbl_fallo> LST = Proposiciones_Get(id_sol.ToString()).Response;
                //return new ResponseGeneric<Crudresponse>("");

                List<sp_config_contrato> contrato = Get_New_Con(id_sol.ToString()).Response; // return

                List<tbl_firmantes> rescon = Get_Firm_Sol(id_sol.ToString()).Response;

                List<tbl_Responsable> rescon_ = Get_Res_Sol(id_sol.ToString()).Response;

                string Json_presupuesto_sol = Get_json_pres_sol(id_sol.ToString()).Response[0].json_pres.ToString();
                //List<PresupuestoContrato> lstPresupuestos = JsonConvert.DeserializeObject<List<PresupuestoContrato>>(LstPres);

                //Lista proveedores
                List<string> listaprov = new List<string>(); // return
                foreach (tbl_fallo prov_ in LST)
                {
                    if (prov_.ganador == true)
                    {
                        tbl_Proveedores lista_p = Get_lista_prov(prov_.rfc.ToString()).Response;
                        listaprov.Add(lista_p.id_proveedor.ToString());
                    }
                }
                // Lista firmantes y responsable
                Responsable_contrato Resp_con = new Responsable_contrato(); // return    
                List<string> lista_firms = new List<string>();

                foreach (tbl_firmantes firm_ in rescon)
                {
                    lista_firms.Add(firm_.firmantes);
                }
                Resp_con.firmantes = lista_firms;
                Resp_con.Responsable = rescon_[0].Responsable;

                Crudresponse Query = Fase_0_ADD(contrato[0]).Response[0];

                if (Query.cod == "success")
                {
                    Resp_con.Contrato = Query.msg;
                    Boolean exito_resp = responsables_contrato(Resp_con);
                    Boolean exito_proveedor = false;
                    Boolean exito_presupuesto = false; //actualizar por presupuesto

                    if (exito_resp)
                    {
                        exito_proveedor = add_contrato_proveedor(listaprov, Guid.Parse(Query.msg));
                        //exito_presupuesto = comprometer_presupuesto_area(lstPresupuestos, Guid.Parse(Query.msg));
                    }
                    else
                    {
                        Query.cod = "error";
                        Query.msg = "Hubo un error al guardar responsable contrato ";
                        return new ResponseGeneric<Crudresponse>(Query);
                    }

                    if (exito_proveedor)
                    {
                        
                        if ((Json_presupuesto_sol != null) || (Json_presupuesto_sol != "")) {
                            exito_presupuesto = get_sp_json_contrato(Guid.Parse(Query.msg), Json_presupuesto_sol.ToString());
                            if (exito_presupuesto)
                            {
                                Query = Negocio_avanzar_fase.avanzar_fase(id_sol.ToString()).Response;
                                return new ResponseGeneric<Crudresponse>(Query);
                            }
                            else
                            {
                                Query.cod = "error";
                                Query.msg = "Hubo un error al guardar presupuestos";
                                return new ResponseGeneric<Crudresponse>(Query);

                            }
                        }
                        else
                        {
                            Query.cod = "error";
                            Query.msg = "Hubo un error al guardar presupuestos";
                            return new ResponseGeneric<Crudresponse>(Query);

                        }
                    }
                    else
                    {

                        Query.cod = "error";
                        Query.msg = "Hubo un error al guardar proveedores";
                        return new ResponseGeneric<Crudresponse>(Query);
                    }
                }
                else
                {
                    Query.cod = "error";
                    Query.msg = "Hubo un error al guardar contrato";
                    return new ResponseGeneric<Crudresponse>(Query);
                }



            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }
        public  Boolean get_sp_json_contrato(Guid p_tbl_contrato_id, String p_json_)
        {
            try
            {
                _acceso_datos.get_sp_json_contrato(p_tbl_contrato_id, p_json_);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
