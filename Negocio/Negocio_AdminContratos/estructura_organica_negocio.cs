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
    public class estructura_organica_negocio
    {
        private estructura_organica_acceso_datos_core _estructura = new estructura_organica_acceso_datos_core();
        private readonly ILoggerManager _logger;

        public estructura_organica_negocio()
        {
            _logger = new LoggerManager();
        }
        //Estructura orgánica
        public ResponseGeneric<List<estructura_organica_core>> Get_lista_estructura_organica(String id_instancia)
        {
            try
            {
                List<estructura_organica_core> _lista_estructura_f = new List<estructura_organica_core>();
                
               

                ResponseGeneric<List<dependencia_estructura>> lista_dependencia = _estructura.Get_dependencias_estructura(id_instancia);
                List<dependencia_estructura> _lista_dep_for = lista_dependencia.Response;

                foreach(dependencia_estructura _dependencia in _lista_dep_for) 
                {
                    List<estructura_area> _lista_area_f = new List<estructura_area>();
                    ResponseGeneric<List<area_estructura>> lista_areas = _estructura.Get_area_estructura(_dependencia.id.ToString());
                    List<area_estructura> _lista_area_for = lista_areas.Response;

                    foreach (area_estructura _area in _lista_area_for)
                    {
                        List<estructura_subarea> _lista_subarea_f = new List<estructura_subarea>();
                        ResponseGeneric<List<subarea_estructura>> lista_subareas = _estructura.Get_subarea_estructura(_area.id.ToString());
                        List<subarea_estructura> _lista_subarea_for = lista_subareas.Response;
                        estructura_area lista_areaas_e = new estructura_area();
                        foreach (subarea_estructura _subarea in _lista_subarea_for)
                        {
                            ResponseGeneric<List<areasubordinada_estructura>> lista_areas_sub = _estructura.Get_area_sub_estructura(_subarea.id.ToString());
                            List<areasubordinada_estructura> lista_dptos = lista_areas_sub.Response;
                            estructura_subarea lista_subareas_e = new estructura_subarea();
                            lista_subareas_e._subarea = new subarea_estructura();
                            lista_subareas_e._subarea = _subarea;
                            lista_subareas_e._oficinas = lista_dptos;
                            _lista_subarea_f.Add(lista_subareas_e);
                        }
                        lista_areaas_e._area = new area_estructura();
                        lista_areaas_e._area = _area;
                        lista_areaas_e._subareas = _lista_subarea_f;
                        _lista_area_f.Add(lista_areaas_e);
                    }

                    estructura_organica_core lista_dependencias = new estructura_organica_core();

                    lista_dependencias._dependencia = new dependencia_estructura();
                    lista_dependencias._dependencia = _dependencia;
                    lista_dependencias._areas = _lista_area_f;
                    _lista_estructura_f.Add(lista_dependencias);
                }

                return new ResponseGeneric<List<estructura_organica_core>>(_lista_estructura_f);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<estructura_organica_core>>(ex);
            }
        }

        //partida dependencia
        public ResponseGeneric<List<CrudresponseIdentificador>> Add_partida_dependencia(tbl_capitulo_gasto_dependencia partidas)
        {
            try
            {
                partidas.p_opt = 2;
                partidas.p_id = Guid.NewGuid();
                return _estructura.Add_Partida_Dependencia(partidas);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }
        public ResponseGeneric<List<CrudresponseIdentificador>> Update_partida_dependencia(List<tbl_capitulo_gasto_dependencia> Lista_partidas)
        {
            try
            {
                List<CrudresponseIdentificador> lista_respuestas = new List<CrudresponseIdentificador>();
                foreach (tbl_capitulo_gasto_dependencia partida in Lista_partidas)
                {
                    partida.p_opt = 3;
                    ResponseGeneric<List<CrudresponseIdentificador>> response = _estructura.Add_Partida_Dependencia(partida);
                    lista_respuestas.Add(response.Response[0]);
                }
                return new ResponseGeneric<List<CrudresponseIdentificador>>(lista_respuestas);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }
        public ResponseGeneric<List<CrudresponseIdentificador>> Delete_partida_dependencia(tbl_capitulo_gasto_dependencia partidas)
        {
            try
            {
                partidas.p_opt = 4;
                return _estructura.Add_Partida_Dependencia(partidas);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }

        //partida área
        public ResponseGeneric<List<CrudresponseIdentificador>> Add_partida_area(List<tbl_capitulo_gasto_area> Lista_partidas)
        {
            try
            {
                List<CrudresponseIdentificador> lista_respuestas = new List<CrudresponseIdentificador>();
                foreach (tbl_capitulo_gasto_area partida in Lista_partidas)
                {
                    Guid existe_cg_a = _estructura.Validar_CG_existente(1, partida.p_tbl_capitulo_gasto_dependencia_id, partida.p_tbl_area_id).Response[0].id_existente;
                    if (existe_cg_a.Equals(Guid.Empty))
                    {
                        partida.p_opt = 2;
                        partida.p_id = Guid.NewGuid();
                        ResponseGeneric<List<CrudresponseIdentificador>> response = _estructura.Add_Partida_Area(partida);
                        lista_respuestas.Add(response.Response[0]);
                    }
                    else
                    {
                        partida.p_opt = 3;
                        partida.p_id = existe_cg_a;
                        ResponseGeneric<List<CrudresponseIdentificador>> response = _estructura.Add_Partida_Area(partida);
                        lista_respuestas.Add(response.Response[0]);
                    }
                }
                return new ResponseGeneric<List<CrudresponseIdentificador>>(lista_respuestas);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }

        //partida subárea
        public ResponseGeneric<List<CrudresponseIdentificador>> Add_partida_subarea(List<tbl_capitulo_gasto_subarea> Lista_partidas)
        {
            try
            {
                List<CrudresponseIdentificador> lista_respuestas = new List<CrudresponseIdentificador>();
                foreach (tbl_capitulo_gasto_subarea partida in Lista_partidas)
                {
                    Guid existe_cg_sa = _estructura.Validar_CG_existente(2, partida.p_tbl_capitulo_gasto_area_id, partida.p_tbl_subarea_id).Response[0].id_existente;
                    if (existe_cg_sa.Equals(Guid.Empty))
                    {
                        partida.p_opt = 2;
                        partida.p_id = Guid.NewGuid();
                        ResponseGeneric<List<CrudresponseIdentificador>> response = _estructura.Add_Partida_Subarea(partida);
                        lista_respuestas.Add(response.Response[0]);
                    }
                    else
                    {
                        partida.p_opt = 3;
                        partida.p_id = existe_cg_sa;
                        ResponseGeneric<List<CrudresponseIdentificador>> response = _estructura.Add_Partida_Subarea(partida);
                        lista_respuestas.Add(response.Response[0]);
                    }
                    
                }
                return new ResponseGeneric<List<CrudresponseIdentificador>>(lista_respuestas);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }

        //partida área subordinada
        public ResponseGeneric<List<CrudresponseIdentificador>> Add_partida_area_sub(List<tbl_capitulo_gasto_area_subordinada> Lista_partidas)
        {
            try
            {
                List<CrudresponseIdentificador> lista_respuestas = new List<CrudresponseIdentificador>();
                foreach (tbl_capitulo_gasto_area_subordinada partida in Lista_partidas)
                {
                    Guid existe_cg_as = _estructura.Validar_CG_existente(1, partida.p_tbl_capitulo_gasto_subarea_id, partida.p_tbl_area_subordinada_id).Response[0].id_existente;
                    if (existe_cg_as.Equals(Guid.Empty))
                    {
                        partida.p_opt = 2;
                        partida.p_id = Guid.NewGuid();
                        ResponseGeneric<List<CrudresponseIdentificador>> response = _estructura.Add_Partida_Area_subordinada(partida);
                        lista_respuestas.Add(response.Response[0]);
                    }
                    else 
                    {
                        partida.p_opt = 3;
                        partida.p_id = existe_cg_as;
                        ResponseGeneric<List<CrudresponseIdentificador>> response = _estructura.Add_Partida_Area_subordinada(partida);
                        lista_respuestas.Add(response.Response[0]);
                    }
                    
                }
                return new ResponseGeneric<List<CrudresponseIdentificador>>(lista_respuestas);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }

        //Lista de capitulos de gastos
        public ResponseGeneric<List<lista_capitulos_gastos>> get_lista_capitulos_gastos(string id_item,string id_ejercicio) 
        {
            try
            {
                return  _estructura.Get_capitulos_gastos(id_item,id_ejercicio);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_capitulos_gastos>>(ex);
            }
        }
        public ResponseGeneric<List<lista_capitulos_gastos_area_estructura>> get_lista_capitulos_gastos_area(string id_dependencia, string id_area, string id_ejercicio)
        {
            try
            {
                List<lista_capitulos_gastos_area_estructura> lista_final = new List<lista_capitulos_gastos_area_estructura>();

                ResponseGeneric<List<lista_capitulos_gastos_area>> lista_areas = _estructura.Get_capitulos_gastos_area(id_dependencia, id_area, id_ejercicio);
                List<lista_capitulos_gastos_area> _lista_dep_for = lista_areas.Response;

                foreach (lista_capitulos_gastos_area lista_pre in _lista_dep_for) {
                    lista_capitulos_gastos_area_estructura lista_sem = new lista_capitulos_gastos_area_estructura();
                    lista_sem.lista1 = lista_pre;
                    Decimal monto_repartido = _estructura.Get_montos_repartidos(1, lista_pre.capitulo_gasto_dependencia_id.ToString(), id_area).Response[0].monto_repartido_a_s;
                    lista_sem.monto_repartido = monto_repartido;
                    lista_final.Add(lista_sem);
                }

                return new ResponseGeneric<List<lista_capitulos_gastos_area_estructura>>(lista_final);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_capitulos_gastos_area_estructura>>(ex);
            }
        }
        public ResponseGeneric<List<lista_capitulos_gastos_subarea_estructura>> get_lista_capitulos_gastos_subarea(string id_dependencia, string id_subarea, string id_ejercicio)
        {
            try
            {
                List<lista_capitulos_gastos_subarea_estructura> lista_final = new List<lista_capitulos_gastos_subarea_estructura>();

                ResponseGeneric<List<lista_capitulos_gastos_subarea>> lista_subareas = _estructura.Get_capitulos_gastos_subarea(id_dependencia, id_subarea, id_ejercicio);
                List<lista_capitulos_gastos_subarea> _lista_dep_for = lista_subareas.Response;

                foreach (lista_capitulos_gastos_subarea lista_pre in _lista_dep_for)
                {
                    lista_capitulos_gastos_subarea_estructura lista_sem = new lista_capitulos_gastos_subarea_estructura();
                    lista_sem.lista1 = lista_pre;
                    Decimal monto_repartido = _estructura.Get_montos_repartidos(2, lista_pre.capitulo_gasto_area_id.ToString(), id_subarea).Response[0].monto_repartido_a_s;
                    lista_sem.monto_repartido = monto_repartido;
                    lista_final.Add(lista_sem);
                }

                return new ResponseGeneric<List<lista_capitulos_gastos_subarea_estructura>>(lista_final);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_capitulos_gastos_subarea_estructura>>(ex);
            }
        }
        public ResponseGeneric<List<lista_capitulos_gastos_area_subordinada>> get_lista_capitulos_gastos_area_sub(string id_dependencia, string id_area_sub, string id_ejercicio)
        {
            try
            {
                return _estructura.Get_capitulos_gastos_area_sub(id_dependencia, id_area_sub, id_ejercicio);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_capitulos_gastos_area_subordinada>>(ex);
            }
        }
        //Lista de capitulos de gastos

        public ResponseGeneric<List<lista_info_capitulo_gasto>> get_lista_info(int o_opt, string id_capitulo_gasto, string id_item)
        {
            try
            {
                return _estructura.Lista_info(o_opt, id_capitulo_gasto, id_item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_info_capitulo_gasto>>(ex);
            }
        }
    }
}
