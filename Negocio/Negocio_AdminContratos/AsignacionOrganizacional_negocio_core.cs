using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using AccesoDatos_AdminContratos;
using System.Linq;
using Utilidades.Log4Net;

namespace Negocio_AdminContratos
{
    public class AsignacionOrganizacional_negocio_core
    {
        private AsignacionOrganizacional_acceso_datos_core _asignacion = new AsignacionOrganizacional_acceso_datos_core();
        private readonly ILoggerManager _logger;

        public AsignacionOrganizacional_negocio_core()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<estructura_lista_dep_areas>> Get_lista_dependencias_areas(String id_instancia)
        {
            try
            {

                ResponseGeneric<List<estructura_tbl_dependencia>> lista_dependencias = _asignacion.Get_lista_dependencias(id_instancia);
                List<estructura_tbl_dependencia> nueva_lista_dep = lista_dependencias.Response;

                ResponseGeneric<List<estructura_tbl_area>> lista_areas = _asignacion.Get_lista_areas(id_instancia);
                List<estructura_tbl_area> nueva_lista_areas = lista_areas.Response;

                Func<List<estructura_tbl_area>, Guid, ETipoItem, List<estructura_lista_dep_areas>> buscar = null;
                buscar = (lista, id, tipo) =>
                {
                    switch (tipo)
                    {
                        case ETipoItem.dependencia:
                            if (lista.Exists(a => a.tbl_dependencia_id == id))
                                return lista.Where(a => a.tbl_dependencia_id == id && (a.id_area_padre == null || a.id_area_padre == "")).Select(a => new estructura_lista_dep_areas()
                                {
                                    Texto = a.area,
                                    IdItem = a.id,
                                    TipoItem = ETipoItem.area,
                                    Hijos = buscar(lista, a.id, ETipoItem.area)
                                }).ToList();
                            break;
                        case ETipoItem.area:
                            if (lista.Exists(a => a.id_area_padre == id.ToString()))
                                return lista.Where(a => a.id_area_padre == id.ToString()).Select(a => new estructura_lista_dep_areas()
                                {
                                    Texto = a.area,
                                    IdItem = a.id,
                                    TipoItem = ETipoItem.area,
                                    Hijos = buscar(lista, a.id, ETipoItem.area)
                                }).ToList();
                            break;
                    }
                    return null;
                };
                var respuesta = new List<estructura_lista_dep_areas>();
                respuesta = new List<estructura_lista_dep_areas>();
                foreach (estructura_tbl_dependencia item in nueva_lista_dep) 
                {
                    respuesta.Add(new estructura_lista_dep_areas() { IdItem = item.id, Texto = item.dependencia, TipoItem = ETipoItem.dependencia });
                }
                foreach (estructura_tbl_area item in nueva_lista_areas)
                {
                    respuesta.ForEach(c => c.Hijos = buscar(nueva_lista_areas, c.IdItem.Value, ETipoItem.dependencia));
                }
                return new ResponseGeneric<List<estructura_lista_dep_areas>>(respuesta);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<estructura_lista_dep_areas>>(ex);
            }
        }

        public ResponseGeneric<List<lista_cap_gastos_dep>> lista_cap_gastos_dep(string tipo,string id_dependencia, string id_instancia, string id_ejercicio) 
        {
            try
            {
                return _asignacion.Get_lista_cap_gastos(tipo,id_dependencia, id_instancia, id_ejercicio);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_cap_gastos_dep>>(ex);
            }
        }
        public ResponseGeneric<List<lista_cap_gastos_areas>> lista_cap_gastos_areas(string tipo, string id_area, string id_instancia, string id_ejercicio)
        {
            try
            {
                return _asignacion.Get_lista_cap_gastos_areas(tipo, id_area, id_instancia, id_ejercicio);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_cap_gastos_areas>>(ex);
            }
        }
        public ResponseGeneric<List<CrudresponseIdentificador>> Add_Partida_Area(List<tbl_partida_area> _tbl_partida_area)
        {
            try
            {
                List<CrudresponseIdentificador> respuesta = new List<CrudresponseIdentificador>();

                foreach (tbl_partida_area lista in _tbl_partida_area)
                {
                    tbl_partida_area nuevo_reg = new tbl_partida_area();
                    var tbl_partida_area_id = _asignacion.Get_existe_partida(lista.p_tbl_area_id, lista.p_tbl_partida_id, lista.p_tbl_ejercicio_id).Response[0].tbl_partida_area_id;
                    if (tbl_partida_area_id.Equals(Guid.Empty))
                    {
                        
                        var monto_disponible = _asignacion.Get_monto_asignado(1, lista.p_tbl_area_id, lista.p_tbl_partida_id, lista.p_tbl_ejercicio_id).Response[0].monto_asignado;
                        if (lista.p_monto_asignado < monto_disponible)
                        {
                            nuevo_reg.p_opt = 2;
                            nuevo_reg.p_id = Guid.NewGuid().ToString();
                            nuevo_reg.p_tbl_area_id = lista.p_tbl_area_id;
                            nuevo_reg.p_tbl_partida_id = lista.p_tbl_partida_id;
                            nuevo_reg.p_tbl_ejercicio_id = lista.p_tbl_ejercicio_id;
                            nuevo_reg.p_id_propietario = lista.p_id_propietario;
                            nuevo_reg.p_monto_planeado = lista.p_monto_planeado;
                            nuevo_reg.p_monto_asignado = lista.p_monto_asignado;
                            nuevo_reg.p_monto_ejercido = lista.p_monto_ejercido;
                            nuevo_reg.p_monto_devengado = lista.p_monto_devengado;
                            nuevo_reg.p_estatus_partida = lista.p_estatus_partida;
                            ResponseGeneric<List<CrudresponseIdentificador>> Step1 = _asignacion.Add_Partida_areas(nuevo_reg);
                            respuesta.Add(Step1.Response[0]);
                        }
                        else 
                        {
                            CrudresponseIdentificador respuesta_conpuesta = new CrudresponseIdentificador();
                            respuesta_conpuesta.cod = "error";
                            respuesta_conpuesta.msg = "Monto superior al disponible";
                            respuesta_conpuesta.id = new Guid(lista.p_tbl_partida_id);
                            respuesta.Add(respuesta_conpuesta);
                        }
                        
                    }
                    else 
                    {
                        var monto_disponible = _asignacion.Get_monto_asignado(1, lista.p_tbl_area_id, lista.p_tbl_partida_id, lista.p_tbl_ejercicio_id).Response[0].monto_asignado;
                        var monto_comprometido = _asignacion.Get_monto_asignado(2, lista.p_tbl_area_id, lista.p_tbl_partida_id, lista.p_tbl_ejercicio_id).Response[0].monto_asignado;
                        if (lista.p_monto_asignado < monto_disponible)
                        {
                            if (lista.p_monto_asignado >= monto_comprometido)
                            {
                                nuevo_reg.p_opt = 3;
                                nuevo_reg.p_id = tbl_partida_area_id.ToString();
                                nuevo_reg.p_tbl_area_id = lista.p_tbl_area_id;
                                nuevo_reg.p_tbl_partida_id = lista.p_tbl_partida_id;
                                nuevo_reg.p_tbl_ejercicio_id = lista.p_tbl_ejercicio_id;
                                nuevo_reg.p_id_propietario = lista.p_id_propietario;
                                nuevo_reg.p_monto_planeado = lista.p_monto_planeado;
                                nuevo_reg.p_monto_asignado = lista.p_monto_asignado;
                                nuevo_reg.p_monto_ejercido = lista.p_monto_ejercido;
                                nuevo_reg.p_monto_devengado = lista.p_monto_devengado;
                                nuevo_reg.p_estatus_partida = lista.p_estatus_partida;
                                ResponseGeneric<List<CrudresponseIdentificador>> Step1 = _asignacion.Add_Partida_areas(nuevo_reg);
                                respuesta.Add(Step1.Response[0]);
                            }
                            else
                            {
                                CrudresponseIdentificador respuesta_conpuesta = new CrudresponseIdentificador();
                                respuesta_conpuesta.cod = "error";
                                respuesta_conpuesta.msg = "Monto inferior al comprometido";
                                respuesta_conpuesta.id = new Guid(lista.p_tbl_partida_id);
                                respuesta.Add(respuesta_conpuesta);
                            }
                        }
                        else
                        {
                            CrudresponseIdentificador respuesta_conpuesta = new CrudresponseIdentificador();
                            respuesta_conpuesta.cod = "error";
                            respuesta_conpuesta.msg = "Monto superior al disponible";
                            respuesta_conpuesta.id = new Guid(lista.p_tbl_partida_id);
                            respuesta.Add(respuesta_conpuesta);
                        }
                    }
                }
                return new ResponseGeneric<List<CrudresponseIdentificador>>(respuesta);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }
    }
}
