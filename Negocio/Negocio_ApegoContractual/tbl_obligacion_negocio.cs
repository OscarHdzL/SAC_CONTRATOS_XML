using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.Log4Net;
using System.Linq;
namespace Solucion_Negocio
{
    public class tbl_obligacion_negocio : crud_tbl_obligacion
    {
        private tbl_obligacion_acceso_datos _Obligaciones = new tbl_obligacion_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_obligacion_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_obligacion>> Consultar(String id)
        {
            try
            {
                return _Obligaciones.Consultar(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return new ResponseGeneric<List<tbl_obligacion>>(ex);
            }
        }


        public ResponseGeneric<List<tbl_obligacion_unitario>> ConsultarId(Guid id)
        {
            try
            {
                return _Obligaciones.ConsultarId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return new ResponseGeneric<List<tbl_obligacion_unitario>>(ex);
            }
        }


        public ResponseGeneric<List<tbl_obligacion_detalle>> ConsultarDetalle(String id)
        {
            try
            {
                return _Obligaciones.ConsultarDetalle(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return new ResponseGeneric<List<tbl_obligacion_detalle>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_obligacion_producto>> ConsultarObligacionProducto(String contrato, String producto)
        {
            try
            {
                return _Obligaciones.ConsultarObligacionProductoDetalle(contrato,producto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return new ResponseGeneric<List<tbl_obligacion_producto>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_periodo>> GetPeriodo()
        {
            try
            {
                return _Obligaciones.GetPeriodo();
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return new ResponseGeneric<List<tbl_periodo>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_tipo_periodo(tbl_tipo_periodo_add tbl_tipo_periodo_add)
        {
            try
            {
                if (tbl_tipo_periodo_add.p_opt == 2)
                {
                    tbl_tipo_periodo_add.p_id = Guid.NewGuid();
                }
                return _Obligaciones.Add_tipo_periodo(tbl_tipo_periodo_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Delete_tipo_periodo(tbl_tipo_periodo_add tbl_tipo_periodo_add)
        {
            try
            {
                return _Obligaciones.Delete_tipo_periodo(tbl_tipo_periodo_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_tipo_obligacion>> GetTipooblig()
        {
            try
            {
                return _Obligaciones.GetTipooblig();
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return new ResponseGeneric<List<tbl_tipo_obligacion>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_tipo_obligacion(tbl_tipo_obligacion_add tbl_tipo_obligacion_add)
        {
            try
            {
                if (tbl_tipo_obligacion_add.p_opt == 2)
                {
                    tbl_tipo_obligacion_add.p_id = Guid.NewGuid();
                }
                return _Obligaciones.Add_tipo_obligacion(tbl_tipo_obligacion_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Delete_tipo_obligacion(tbl_tipo_obligacion_add tbl_tipo_obligacion_add)
        {
            try
            {
                return _Obligaciones.Delete_tipo_obligacion(tbl_tipo_obligacion_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<verificar_oblig> VerificarObligacion(Guid idObligacion)
        {
            try
            {
                return _Obligaciones.VerificarObligacion(idObligacion);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return new ResponseGeneric<verificar_oblig>(ex);
            }
        }
        public List<DropDownList> GetAreasResponsables(String id)
        {
            try
            {
                return _Obligaciones.GetAreasResponsables(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return new List<DropDownList>();
            }
        }
        public List<DropDownList> GetServidoresResponsables(String id)
        {
            try
            {
                return _Obligaciones.GetServidoresResponsables(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return new List<DropDownList>();
            }
        }
        public ResponseGeneric<Dictionary<String, String>> Update(tbl_obligacion_input_conatiner input, int p_opt) 
        {
            try
            {
                Guid Obligacion = Guid.NewGuid();
                Guid Obligacion_link = Guid.NewGuid();
                if (p_opt == 2)
                {
                    input.tbl_obligacion.p_id = Obligacion;
                    input.tbl_obligacion.p_opt = p_opt;

                    input.tbl_link_obligacion.p_tbl_obligacion_id = Obligacion;
                    input.tbl_link_obligacion.p_opt = p_opt;
                    input.tbl_link_obligacion.p_id = Obligacion_link;
                }
                else
                {
                    Obligacion = input.tbl_obligacion.p_id;
                    Obligacion_link = input.tbl_link_obligacion.p_id;
                    input.tbl_obligacion.p_opt = p_opt;
                    input.tbl_link_obligacion.p_opt = p_opt;


                }

                Dictionary<String, String> Response = new Dictionary<String, String>();


                ResponseGeneric<List<Crudresponse>> Step_1 = _Obligaciones.Add(input.tbl_obligacion);
                Response.Add("Step_1", Step_1.Response[0].cod + "|" + Step_1.Response[0].msg);
                if(Step_1.Response[0].cod != "success")
                {
                    return new ResponseGeneric<Dictionary<String, String>>(Response);
                }

         
                   // input.tbl_link_obligacion.p_str_areas = input.tbl_area_obligacion.p_str_areas;
                   // input.tbl_link_obligacion.p_str_responsables = input.tbl_responsable_obligacion.p_str_responsables;

                

                ResponseGeneric<List<Crudresponse>> Step_2 = _Obligaciones.AddObligacioLink(input.tbl_link_obligacion);
                Response.Add("Step_2", Step_2.Response[0].cod + "|" + Step_2.Response[0].msg);
                if (Step_2.Response[0].cod != "success")
                {
                    return new ResponseGeneric<Dictionary<String, String>>(Response);
                }


                //ResponseGeneric<List<Crudresponse>> Step_3 = _Obligaciones.AddObligacionAreas(Obligacion,input.tbl_area_obligacion.p_str_areas, p_opt);
                //Response.Add("Step_3", Step_3.Response[0].cod + "|" + Step_3.Response[0].msg);
                //if (Step_3.Response[0].cod != "success")
                //{
                //    return new ResponseGeneric<Dictionary<String, String>>(Response);
                //}

                //ResponseGeneric<List<Crudresponse>> Step_4 = _Obligaciones.AddObligacioResponsables(Obligacion, input.tbl_responsable_obligacion.p_str_responsables, p_opt);
                //Response.Add("Step_4", Step_4.Response[0].cod + "|" + Step_4.Response[0].msg);



                return new ResponseGeneric<Dictionary<String, String>>(Response);

            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return new ResponseGeneric<Dictionary<String, String>>(ex);
            }
        }
        public ResponseGeneric<Dictionary<String, Crudresponse>> Put(tbl_obligacion_input_conatiner input)
        {
            try
            {
                int p_opt = 2;
                Guid Obligacion = Guid.NewGuid();
                input.tbl_obligacion.p_id = Obligacion;
                input.tbl_obligacion.p_opt = p_opt;

                input.tbl_link_obligacion.p_tbl_obligacion_id = Obligacion;
                input.tbl_link_obligacion.p_opt = p_opt;


                ResponseGeneric<List<Crudresponse>> Step_1 = _Obligaciones.Add(input.tbl_obligacion);
                ResponseGeneric<List<Crudresponse>> Step_2 = _Obligaciones.AddObligacioLink(input.tbl_link_obligacion);
                ResponseGeneric<List<Crudresponse>> Step_3 = _Obligaciones.AddObligacionAreas(Obligacion, input.tbl_area_obligacion.p_str_areas, p_opt);
                ResponseGeneric<List<Crudresponse>> Step_4 = _Obligaciones.AddObligacioResponsables(Obligacion, input.tbl_responsable_obligacion.p_str_responsables, p_opt);
                Dictionary<String, Crudresponse> Response = new Dictionary<String, Crudresponse>();
                Response.Add("Step_1", Step_1.Response[0]);
                Response.Add("Step_2", Step_2.Response[0]);
                Response.Add("Step_3", Step_3.Response[0]);
                Response.Add("Step_4", Step_4.Response[0]);

                return new ResponseGeneric<Dictionary<String, Crudresponse>>(Response);

            }
            catch (Exception ex)
            {
                _logger.LogError("put", ex);
                return new ResponseGeneric<Dictionary<String, Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete(Guid id)
        {
            try
            {
                return _Obligaciones.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_tipo_aplicacion>> Get_tipo_aplicacion()
        {
            try
            {
                return _Obligaciones.Get_tipo_aplicacion();
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<tbl_tipo_aplicacion>>(ex);
            }
        }



        public ResponseGeneric<List<ReporteSancionesConsulta>> Get_reporte_sanciones(String contrato, String fecha_filtro)
        {
            try
            {
                return _Obligaciones.ReporteSanciones(contrato, fecha_filtro);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get_reporte_sanciones", ex);
                return new ResponseGeneric<List<ReporteSancionesConsulta>>(ex);
            }
        }

        public ResponseGeneric<List<ReporteSancionesBody>> Get_reporte_sanciones_agrupado(String contrato, String fecha_filtro)
        {
            try
            {
                var resultado = _Obligaciones.ReporteSanciones(contrato, fecha_filtro);
                if (resultado.Response.Count > 0)
                {
                    var resultadoQuery = resultado.Response;
                    var planesDiferentes = resultadoQuery.GroupBy(p => p.tbl_plan_entrega_id).Select(x => x.FirstOrDefault().tbl_plan_entrega_id);
                    List<ReporteSancionesBody> resultados = new List<ReporteSancionesBody>();
                    foreach (var elementoH in planesDiferentes)
                    {
                        var listaPlanes = resultadoQuery.Where(x => x.tbl_plan_entrega_id == elementoH).ToList();
                        var ubicacionesDiferentes = listaPlanes.Where(x => x.tbl_plan_entrega_id == elementoH).GroupBy(p => p.tbl_ubicacion_id).Select(x => x.FirstOrDefault().tbl_ubicacion_id);

                        ReporteSancionesBody body = new ReporteSancionesBody();
                        body.tbl_contrato_id = listaPlanes[0].tbl_contrato_id;
                        body.tbl_plan_entrega_id = listaPlanes[0].tbl_plan_entrega_id;
                        body.tbl_contrato_servidor_resp_id = listaPlanes[0].tbl_contrato_servidor_resp_id;
                        body.identificador = listaPlanes[0].identificador;
                        body.periodo = listaPlanes[0].periodo;
                        body.descripcion = listaPlanes[0].descripcion;
                        body.fecha_ejecucion = listaPlanes[0].fecha_ejecucion;
                        body.dias_restantes_ejecucion = listaPlanes[0].dias_restantes_ejecucion;
                        body.tbl_plan_entrega_inclusion = listaPlanes[0].tbl_plan_entrega_inclusion;
                        body.tbl_plan_entrega_activo = listaPlanes[0].tbl_plan_entrega_activo;
                        body.tipo_entrega = listaPlanes[0].tipo_entrega;
                        body.cumplio_pe = listaPlanes[0].cumplio_pe;
                        body.ejecutado = listaPlanes[0].ejecutado;
                        body.Ubicaciones = new List<ReporteSancionesUbicaciones>();

                        foreach (var elementoU in ubicacionesDiferentes)
                        {
                            ReporteSancionesUbicaciones ubicaciones = new ReporteSancionesUbicaciones();
                            var listaUbicaciones = listaPlanes.Where(x => x.tbl_ubicacion_id == elementoU).ToList();
                            ubicaciones.tbl_ubicacion_id = elementoU;
                            ubicaciones.tbl_ubicacion_clave = listaUbicaciones[0].tbl_ubicacion_clave;
                            ubicaciones.tbl_ubicacion_unidad = listaUbicaciones[0].tbl_ubicacion_unidad;
                            ubicaciones.Productos = new List<ReporteSancionesProducto>();

                            var productosDiferentes = listaUbicaciones.GroupBy(p => p.tbl_contrato_producto_id).Select(x => x.FirstOrDefault().tbl_contrato_producto_id);
                            
                            foreach (var _producto in productosDiferentes) {
                                var productoDetalle = listaUbicaciones.Where(x => x.tbl_contrato_producto_id == _producto).FirstOrDefault();
                                var productosUbicacion = listaUbicaciones.Where(x => x.tbl_contrato_producto_id == _producto).ToList();
                                ReporteSancionesProducto sancionesProducto = new ReporteSancionesProducto();
                                sancionesProducto.tbl_contrato_producto_id = _producto;
                                sancionesProducto.tbl_plan_entrega_producto_id = productoDetalle.tbl_plan_entrega_producto_id;
                                sancionesProducto.tbl_contrato_producto_id = productoDetalle.tbl_contrato_producto_id;
                                sancionesProducto.tbl_ubicacion_plan_entrega_id = productoDetalle.tbl_ubicacion_plan_entrega_id;
                                sancionesProducto.tbl_plan_entrega_producto_estatus = productoDetalle.tbl_plan_entrega_producto_estatus;
                                sancionesProducto.tbl_plan_entrega_producto_inclusion = productoDetalle.tbl_plan_entrega_producto_inclusion;
                                sancionesProducto.cantidad = productoDetalle.cantidad;
                                sancionesProducto.detalle_actividad = productoDetalle.detalle_actividad;
                                sancionesProducto.tipo = productoDetalle.tipo;
                                sancionesProducto.cumplio = productoDetalle.cumplio;

                                sancionesProducto.producto_servicio_id = productoDetalle.producto_servicio_id;
                                sancionesProducto.producto_servicio_nombre = productoDetalle.producto_servicio_nombre;
                                sancionesProducto.producto_servicio_clave = productoDetalle.producto_servicio_clave;

                                sancionesProducto.Obligaciones = new List<ReporteSancionesObligacion>();
                                foreach (var sancion in productosUbicacion) {
                                    ReporteSancionesObligacion obligacion = new ReporteSancionesObligacion();
                                    obligacion.tbl_link_obligacion_id = sancion.tbl_link_obligacion_id;
                                    obligacion.tbl_obligacion_id = sancion.tbl_obligacion_id;
                                    obligacion.clausula = sancion.clausula;
                                    obligacion.nivel_servicio = sancion.nivel_servicio;
                                    obligacion.forma_aplicacion = sancion.forma_aplicacion;
                                    obligacion.comentarios = sancion.comentarios;
                                    obligacion.inclusion = sancion.inclusion;
                                    obligacion.obligacion = sancion.obligacion;
                                    obligacion.monto = sancion.monto;
                                    obligacion.porcentaje = sancion.porcentaje;
                                    obligacion.tbl_tipo_obligacion_id = sancion.tbl_tipo_obligacion_id;
                                    obligacion.tipo_obligacion = sancion.tipo_obligacion;
                                    obligacion.tbl_sancion_obligacion_id = sancion.tbl_sancion_obligacion_id;
                                    obligacion.sansion = sancion.sansion;
                                    obligacion.tbl_periodo_id = sancion.tbl_periodo_id;
                                    obligacion.tbl_periodo_periodo = sancion.tbl_periodo_periodo;
                                    obligacion.tbl_tipo_prioridad_id = sancion.tbl_tipo_prioridad_id;
                                    obligacion.tbl_tipo_prioridad_nombre = sancion.tbl_tipo_prioridad_nombre;
                                    obligacion.obligacion_cumplida = sancion.obligacion_cumplida;
                                    sancionesProducto.Obligaciones.Add(obligacion);
                                }


                                ubicaciones.Productos.Add(sancionesProducto);
                            }


                            body.Ubicaciones.Add(ubicaciones);

                        }


                        resultados.Add(body);
                    }
                    return new ResponseGeneric<List<ReporteSancionesBody>>(resultados);
                }
                else {
                    var r = new List<ReporteSancionesBody>();
                    return new ResponseGeneric<List<ReporteSancionesBody>>(r);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get_reporte_sanciones_agrupado", ex);
                return new ResponseGeneric<List<ReporteSancionesBody>>(ex);
            }
        }
    }
}
