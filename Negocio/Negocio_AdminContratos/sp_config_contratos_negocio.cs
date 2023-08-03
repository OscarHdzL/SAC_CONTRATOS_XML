using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Area;
using Modelos.Response;
using AccesoDatos_AdministracionDeContratos;
using Modelos.Modelos.ServidoresPublicos;
using Modelos.Modelos.Contrato;
using System.Linq;
using Utilidades.Log4Net;

namespace NegocioAdministracionContratos
{
    public class sp_config_contratos_negocio 
    {
        private sp_contrato_config_datos sp_contrato_config_datos_ = new sp_contrato_config_datos();
        private readonly ILoggerManager _logger;

        public sp_config_contratos_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_servidor_publico>> ConsultarServPub(Guid dependencia)
        {
            try
            {
                return sp_contrato_config_datos_.ConsultarServPub(dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_servidor_publico>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Fase_0_ADD(sp_config_contrato contrato)
        {
            try
            {
                contrato.p_id = Guid.NewGuid();
                return sp_contrato_config_datos_.config(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        //public ResponseGeneric<List<Crudresponse>> AsignarE(AsignacionAreaContrato area) 
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex) {
        //        _logger.LogError("AsignarE", ex);
        //        return new ResponseGeneric<List<Crudresponse>>(ex);
        //    }
        //}
        public ResponseGeneric<List<Crudresponse>> Fase_0_Update(sp_config_contrato contrato)
        {
            try
            {
                return sp_contrato_config_datos_.config(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }


        public ResponseGeneric<List<Crudresponse>> Fase_0_file(sp_config_contrato contrato)
        {
            try
            {
                return sp_contrato_config_datos_.config(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_tipo_contrato>> tipocontrato()
        {
            try
            {
                return sp_contrato_config_datos_.ConsultartipoContrato();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_contrato>>(ex);
            }
        }

        public ResponseGeneric<List<ContratoPresupuesto>> get_partidas_montos_area(Guid iddep)
        {
            try
            {
                return sp_contrato_config_datos_.get_partidas_montos_area(iddep);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ContratoPresupuesto>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_proveedor>> proveedoresdep(Guid idproveedor)
        {
            try
            {
                return sp_contrato_config_datos_.proveedoresdep(idproveedor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_proveedor>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> get_adicionalescontrato(String opt)
        {
            try
            {
                return sp_contrato_config_datos_.get_adicionalescontrato(opt);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<sp_config_contrato_>> get_contratoporId(Guid contratoid)
        {
            try
            {
                return sp_contrato_config_datos_.get_contratoporId(contratoid);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_config_contrato_>>(ex);
            }
        }

        public Boolean responsables_contrato(Responsable_contrato rc)
        {
            try
            {
                foreach (String val in rc.firmantes)
                {
                    sp_contrato_config_datos_.responsables_contrato(Guid.Parse(val), Guid.Parse(rc.Contrato), 0, "insert");
                }
                sp_contrato_config_datos_.responsables_contrato(Guid.Parse(rc.Responsable), Guid.Parse(rc.Contrato), 1, "insert");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return false;
            }
        }

        public Boolean add_contrato_proveedor(List<Guid> lst, Guid contrato)
        {
            try
            {
                foreach (Guid val in lst)
                {
                    sp_contrato_config_datos_.add_contrato_proveedor(val, contrato, "insert");
                }
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return false;
            }
        }

        public Boolean responsables_contrato_update(Responsable_contrato rc)
        {
            try
            {
                sp_contrato_config_datos_.responsables_contrato(Guid.Empty, Guid.Parse(rc.Contrato), 0, "reset");

                foreach (String val in rc.firmantes)
                {
                    sp_contrato_config_datos_.responsables_contrato(Guid.Parse(val), Guid.Parse(rc.Contrato), 0, "insert");
                }
                sp_contrato_config_datos_.responsables_contrato(Guid.Parse(rc.Responsable), Guid.Parse(rc.Contrato), 1, "insert");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return false;
            }
        }

        public Boolean add_contrato_proveedor_update(List<Guid> lst, Guid contrato)
        {
            try
            {
                sp_contrato_config_datos_.add_contrato_proveedor(Guid.Empty, contrato, "reset");
                foreach (Guid val in lst)
                {
                    sp_contrato_config_datos_.add_contrato_proveedor(val, contrato, "insert");
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return false;
            }
        }


        public Boolean get_proveedores_por_contrato(Guid contrato)
        {
            try
            {
                sp_contrato_config_datos_.get_proveedores_por_contrato(contrato);
              

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
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
                    sp_contrato_config_datos_.comprometer_presupuesto_area(Cmp);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return false;
            }
        }


        public ResponseGeneric<List<tbl_contrato_list>> Get_lista_contratos(string dependencia)
        {
            try
            {
                return sp_contrato_config_datos_.get_lista_contratos(dependencia);

            }

            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_contrato_list>>(ex);
            }
        }
        public ResponseGeneric<List<CrudresponseNum>> get_sp_json_contrato(Guid p_tbl_contrato_id, String p_json_)
        {
            try
            {
                return sp_contrato_config_datos_.get_sp_json_contrato(p_tbl_contrato_id, p_json_);
    
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<CrudresponseNum>>(ex);
            }
        }
        public ResponseGeneric<List<CrudresponseNum>> get_sp_json_contrato(Guid p_tbl_contrato_id)
        {
            try
            {
                return sp_contrato_config_datos_.sp_get_json_contrato(p_tbl_contrato_id);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<CrudresponseNum>>(ex);
            }
        }

        public List<String> get_contrProveedor(Guid p_tbl_contrato_id)
        {
            try
            {
                List<String> Lst = new List<String>();
                foreach (CrudresponseNum cod in sp_contrato_config_datos_.get_contrProveedor(p_tbl_contrato_id).Response)
                {
                    Lst.Add(cod.cod);
                }
                return Lst;

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new List<String>();
            }
        }
        public Responsable_contrato sp_get_responsable_contrato_(Guid p_tbl_contrato_id)
        {
            Responsable_contrato Response_ = new Responsable_contrato();
            Response_.firmantes = new List<String>();
            try
            {
                List<Crudresponse> RCC = sp_contrato_config_datos_.sp_get_responsable_contrato_(p_tbl_contrato_id).Response.ToList();
                foreach (Crudresponse CR in RCC.Where(x => x.msg == "0"))
                {
                    Response_.firmantes.Add(CR.cod);
                }
                Response_.Responsable = RCC.Where(x => x.msg == "1").FirstOrDefault().cod;
                Response_.Contrato = p_tbl_contrato_id.ToString();
                return Response_;

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new Responsable_contrato();
            }
        }
        public ResponseGeneric<List<CrudresponseNum>> upd_token_contrato(Guid p_id, String p_token)
        {
            Responsable_contrato Response_ = new Responsable_contrato();
            return sp_contrato_config_datos_.upd_token_contrato(p_id, p_token);
        }
    }
}


 
