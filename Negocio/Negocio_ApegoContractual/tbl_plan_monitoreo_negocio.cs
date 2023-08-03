using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_plan_monitoreo_negocio
    {
        private sp_plan_monitoreo_acceso_datos _monitoreo = new sp_plan_monitoreo_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_plan_monitoreo_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_plan_monitoreo_estado>> ConsultarEstado()
        {
            try
            {
                return _monitoreo.ConsultarEstado();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_plan_monitoreo_estado>>(ex);
            }
        }

        public ResponseGeneric<List<CrudresponseIdentificador>> sp_plan_monitoreo(sp_plan_monitoreo_struc plan)
        {
            try
            {
                List<CrudresponseIdentificador> lista = new List<CrudresponseIdentificador>();

                plan._plan_monitoreo.p_opt = 2;

                ResponseGeneric<CrudresponseIdentificador> Step1 = _monitoreo.sp_plan_monitoreo(plan._plan_monitoreo);

                lista.Add(Step1.Response);

                Guid idPlanMon = Step1.Response.id;


                foreach (sp_plan_monitoreo_ubicacion ubicacion in plan.ubicaciones)
                {
                    ubicacion.p_opt = 2;
                    ubicacion.p_tbl_plan_monitoreo_id = idPlanMon;
                    ResponseGeneric<CrudresponseIdentificador> Step2 = _monitoreo.sp_plan_monitoreo_ubicaciones(ubicacion);
                    lista.Add(Step2.Response);
                }


                return new ResponseGeneric<List<CrudresponseIdentificador>>(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }



        public ResponseGeneric<List<tbl_plan_monitoreo_lista>> ConsultarPlanes_Monitoreo(Guid contrato)
        {
            try
            {
                return _monitoreo.ConsultarPlanes_Monitoreo(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_plan_monitoreo_lista>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_ubicaciones_planmonitoreo>> ConsultarUbicaciones_PlanMonitoreo (Guid idPlanMonitoreo)
        {
            try
            {
                return _monitoreo.ConsultarUbicaciones_PlanMonitoreo(idPlanMonitoreo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_ubicaciones_planmonitoreo>>(ex);
            }
        }

        public ResponseGeneric<List<sp_productos_ubicacion_monitoreo>> ConsultarProductos_Ubic_PlanMon(Guid idPlanMonitoreo, Guid idUbicacion)
        {
            try
            {
                return _monitoreo.ConsultarProductos_Ubic_PlanMon(idPlanMonitoreo, idUbicacion);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_productos_ubicacion_monitoreo>>(ex);
            }
        }

        public ResponseGeneric<List<sp_obligaciones_ubicacion_producto_planmonitoreo>> ConsultarObligaciones_Ubic_Producto(Guid idPlanMonitoreo, Guid idUbicacion, Guid idUProducto)
        {
            try
            {
                return _monitoreo.ConsultarObligaciones_Ubic_Producto(idPlanMonitoreo, idUbicacion, idUProducto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_obligaciones_ubicacion_producto_planmonitoreo>>(ex);
            }
        }

        public ResponseGeneric<List<sp_obligaciones_nocumple>> ConsultarObligaciones_NoCumple(Guid idPlanMonitoreo, Guid idUProducto)
        {
            try
            {
                return _monitoreo.ConsultarObligaciones_NoCumple(idPlanMonitoreo, idUProducto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_obligaciones_nocumple>>(ex);
            }
        }

        public ResponseGeneric<List<sp_obligaciones_incumplidas>> ConsultarObligaciones_Incumplidas(Guid idPlanMonitoreo)
        {
            try
            {
                return _monitoreo.ConsultarObligaciones_Incumplidas(idPlanMonitoreo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_obligaciones_incumplidas>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_Archivo_PM_(sp_tbl_ArchivosMonitoreo tbl_ArchivosPM)
        {
            try
            {
                tbl_ArchivosPM.id_ = Guid.NewGuid();
                return _monitoreo.add_archivosPM_(tbl_ArchivosPM);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<File_name>> _sp_download_filename_monitoreo(string monitoreo)
        {
            try
            {
                return _monitoreo._sp_download_filename_monitoreo(monitoreo);
            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<File_name>>(ex);
            }
        }

    }
}
