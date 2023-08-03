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
    public class sp_confirmarPE_negocio
    {
        private sp_confirmar_pe_acceso_datos NegocionPlanEntrega = new sp_confirmar_pe_acceso_datos();
        private sp_plan_monitoreo_acceso_datos NegocioPM = new sp_plan_monitoreo_acceso_datos();
        private readonly ILoggerManager _logger;

        public sp_confirmarPE_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<Crudresponse> Confirmar(String PE)
        {
            try
            {
                ResponseGeneric<Crudresponse> operacion = NegocionPlanEntrega.confirmar_pe(PE);
                if (operacion.CurrentException == null) {
                    var PEAutomatico = NegocioPM.sp_plan_monitoreo_automatico(PE);
                    if (PEAutomatico.CurrentException == null) {
                        Console.WriteLine("Todo ok");
                    }
                }
                return operacion;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }
        public ResponseGeneric<List<tbl_obligacion_cls_PE>> get_obligacion_PE_Incumplidas(int p_opt, Guid tbl_plan_entrega_id_, Guid plan_entrega_producto_id)
        {
            ResponseGeneric<List<tbl_obligacion_cls_PE>> tbl_obligacion_PE_Inc = NegocionPlanEntrega.get_obligacion_Incumplidas(p_opt, tbl_plan_entrega_id_, plan_entrega_producto_id);
            return tbl_obligacion_PE_Inc;
        }

        public ResponseGeneric<Crudresponse> Eliminar_producto_plan_entrega(String tbl_plan_entrega_id, String plan_entrega_producto_id, String tbl_ubicacion_plan_entrega_id, String tbl_contrato_producto_id)
        {
            try
            {
                return NegocionPlanEntrega.Eliminar_producto_plan_entrega(tbl_plan_entrega_id, plan_entrega_producto_id, tbl_ubicacion_plan_entrega_id, tbl_contrato_producto_id);
            }
            catch (Exception ex) {
                _logger.LogError("Eliminar_producto_plan_entrega", ex);
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

        public ResponseGeneric<Crudresponse> Eliminar_ubicacion_plan_entrega(String tbl_plan_entrega_id, String tbl_ubicacion_id) 
        {
            try
            {
                return NegocionPlanEntrega.Eliminar_ubicacion_plan_entrega(tbl_plan_entrega_id, tbl_ubicacion_id);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Eliminar_ubicacion_plan_entrega", ex);
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }
        public ResponseGeneric<Crudresponse> Eliminar_plan_entrega(String tbl_plan_entrega_id) 
        {
            try
            {
                return NegocionPlanEntrega.Eliminar_plan_entrega(tbl_plan_entrega_id);
            }
            catch (Exception ex) {
                _logger.LogError("Eliminar_plan_entrega", ex);
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }



    }
}
