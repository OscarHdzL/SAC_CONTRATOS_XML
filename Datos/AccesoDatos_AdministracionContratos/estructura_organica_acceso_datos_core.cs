using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.Dependencia;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos_AdminContratos
{
    public class estructura_organica_acceso_datos_core
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_dependencia_estructura = "sp_get_dependencia_estructura";
        string sp_get_area_estructura = "sp_get_area_estructura";
        string sp_get_subarea_estructura = "sp_get_subarea_estructura";
        string sp_get_area_subordinada_estructura = "sp_get_area_subordinada_estructura";
        string sp_asignar_presupuesto_dependencia = "sp_asignar_presupuesto_dependencia";
        string sp_asignar_presupuesto_areas = "sp_asignar_presupuesto_area";
        string sp_asignar_presupuesto_subarea = "sp_asignar_presupuesto_subarea";
        string sp_asignar_presupuesto_area_subordinada = "sp_asignar_presupuesto_area_subordinada";
        string sp_get_lista_capitulos_gastos_dependencia = "sp_get_lista_capitulos_gastos_dependencia";
        string sp_get_lista_capitulos_gastos_area = "sp_get_lista_capitulos_gastos_area";
        string sp_get_lista_capitulos_gastos_subarea = "sp_get_lista_capitulos_gastos_subarea";
        string sp_get_lista_capitulos_gastos_area_subordinada = "sp_get_lista_capitulos_gastos_area_subordinada";
        string sp_get_monto_repartido_cg = "sp_get_monto_repartido_cg";
        string sp_get_existe_capitulo_gasto = "sp_get_existe_capitulo_gasto";
        string sp_get_lista_info_cg_repartidos = "sp_get_lista_info_cg_repartidos";
        /*Estructura organica*/

        public estructura_organica_acceso_datos_core()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<dependencia_estructura>> Get_dependencias_estructura(String id_instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = id_instancia.ToString() });
                List<dependencia_estructura> Lista = new List<dependencia_estructura>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_dependencia_estructura);
                            Lista = conexion.Query<dependencia_estructura>().FromSql<dependencia_estructura>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<dependencia_estructura>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<dependencia_estructura>>(ex);
            }
        }
        public ResponseGeneric<List<area_estructura>> Get_area_estructura(String id_dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = id_dependencia.ToString() });
                List<area_estructura> Lista = new List<area_estructura>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_area_estructura);
                            Lista = conexion.Query<area_estructura>().FromSql<area_estructura>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<area_estructura>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<area_estructura>>(ex);
            }
        }
        public ResponseGeneric<List<subarea_estructura>> Get_subarea_estructura(String id_area)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = id_area.ToString() });
                List<subarea_estructura> Lista = new List<subarea_estructura>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_subarea_estructura);
                            Lista = conexion.Query<subarea_estructura>().FromSql<subarea_estructura>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<subarea_estructura>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<subarea_estructura>>(ex);
            }
        }
        public ResponseGeneric<List<areasubordinada_estructura>> Get_area_sub_estructura(String id_subarea)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_subarea_id", Tipo = "String", Valor = id_subarea.ToString() });
                List<areasubordinada_estructura> Lista = new List<areasubordinada_estructura>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_area_subordinada_estructura);
                            Lista = conexion.Query<areasubordinada_estructura>().FromSql<areasubordinada_estructura>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<areasubordinada_estructura>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<areasubordinada_estructura>>(ex);
            }
        }
        /*Fin de la estructura organica*/

        /*Asignacion de presupuestos*/
        public ResponseGeneric<List<CrudresponseIdentificador>> Add_Partida_Dependencia(tbl_capitulo_gasto_dependencia _tbl_partida)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = _tbl_partida.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_partida.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = _tbl_partida.p_tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_capitulo_gasto_id", Tipo = "String", Valor = _tbl_partida.p_tbl_capitulo_gasto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = _tbl_partida.p_tbl_ejercicio_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_asignado", Tipo = "String", Valor = _tbl_partida.p_monto_asignado.ToString() });
                List<CrudresponseIdentificador> Lista = new List<CrudresponseIdentificador>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_asignar_presupuesto_dependencia);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<CrudresponseIdentificador>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }
        public ResponseGeneric<List<CrudresponseIdentificador>> Add_Partida_Area(tbl_capitulo_gasto_area _tbl_partida)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = _tbl_partida.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_partida.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_capitulo_gasto_dependencia_id", Tipo = "String", Valor = _tbl_partida.p_tbl_capitulo_gasto_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = _tbl_partida.p_tbl_area_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_asignado", Tipo = "String", Valor = _tbl_partida.p_monto_asignado.ToString() });
                List<CrudresponseIdentificador> Lista = new List<CrudresponseIdentificador>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_asignar_presupuesto_areas);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<CrudresponseIdentificador>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }
        public ResponseGeneric<List<CrudresponseIdentificador>> Add_Partida_Subarea(tbl_capitulo_gasto_subarea _tbl_partida)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = _tbl_partida.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_partida.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_capitulo_gasto_area_id", Tipo = "String", Valor = _tbl_partida.p_tbl_capitulo_gasto_area_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_subarea_id", Tipo = "String", Valor = _tbl_partida.p_tbl_subarea_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_asignado", Tipo = "String", Valor = _tbl_partida.p_monto_asignado.ToString() });
                List<CrudresponseIdentificador> Lista = new List<CrudresponseIdentificador>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_asignar_presupuesto_subarea);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<CrudresponseIdentificador>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }
        public ResponseGeneric<List<CrudresponseIdentificador>> Add_Partida_Area_subordinada(tbl_capitulo_gasto_area_subordinada _tbl_partida)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = _tbl_partida.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_partida.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_capitulo_gasto_subarea_id", Tipo = "String", Valor = _tbl_partida.p_tbl_capitulo_gasto_subarea_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_subordinada_id", Tipo = "String", Valor = _tbl_partida.p_tbl_area_subordinada_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_asignado", Tipo = "String", Valor = _tbl_partida.p_monto_asignado.ToString() });
                List<CrudresponseIdentificador> Lista = new List<CrudresponseIdentificador>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_asignar_presupuesto_area_subordinada);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<CrudresponseIdentificador>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }
        /*Fin de asignacion de presupuestos*/

        /*Lista de capitulos de gastos para asignación de presupuestos*/
        public ResponseGeneric<List<lista_capitulos_gastos>> Get_capitulos_gastos(String id_item, String id_ejercicio)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_item", Tipo = "String", Valor = id_item.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = id_ejercicio.ToString() });
                List<lista_capitulos_gastos> Lista = new List<lista_capitulos_gastos>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_lista_capitulos_gastos_dependencia);
                            Lista = conexion.Query<lista_capitulos_gastos>().FromSql<lista_capitulos_gastos>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<lista_capitulos_gastos>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<lista_capitulos_gastos>>(ex);
            }
        }
        public ResponseGeneric<List<lista_capitulos_gastos_area>> Get_capitulos_gastos_area(String id_dependencia,String id_area, String id_ejercicio)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_dependencia_id", Tipo = "String", Valor = id_dependencia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = id_area.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = id_ejercicio.ToString() });
                List<lista_capitulos_gastos_area> Lista = new List<lista_capitulos_gastos_area>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_lista_capitulos_gastos_area);
                            Lista = conexion.Query<lista_capitulos_gastos_area>().FromSql<lista_capitulos_gastos_area>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<lista_capitulos_gastos_area>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<lista_capitulos_gastos_area>>(ex);
            }
        }
        public ResponseGeneric<List<lista_capitulos_gastos_subarea>> Get_capitulos_gastos_subarea(String id_dependencia, String id_subarea, String id_ejercicio)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_dependencia_id", Tipo = "String", Valor = id_dependencia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_subarea_id", Tipo = "String", Valor = id_subarea.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = id_ejercicio.ToString() });
                List<lista_capitulos_gastos_subarea> Lista = new List<lista_capitulos_gastos_subarea>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_lista_capitulos_gastos_subarea);
                            Lista = conexion.Query<lista_capitulos_gastos_subarea>().FromSql<lista_capitulos_gastos_subarea>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<lista_capitulos_gastos_subarea>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<lista_capitulos_gastos_subarea>>(ex);
            }
        }
        public ResponseGeneric<List<lista_capitulos_gastos_area_subordinada>> Get_capitulos_gastos_area_sub(String id_dependencia, String id_area_sub, String id_ejercicio)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_dependencia_id", Tipo = "String", Valor = id_dependencia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_subordinada_id", Tipo = "String", Valor = id_area_sub.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = id_ejercicio.ToString() });
                List<lista_capitulos_gastos_area_subordinada> Lista = new List<lista_capitulos_gastos_area_subordinada>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_lista_capitulos_gastos_area_subordinada);
                            Lista = conexion.Query<lista_capitulos_gastos_area_subordinada>().FromSql<lista_capitulos_gastos_area_subordinada>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<lista_capitulos_gastos_area_subordinada>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<lista_capitulos_gastos_area_subordinada>>(ex);
            }
        }
        /*Fin de la lista de capitulos de gastos para asignación de presupuestos*/

        /*Auxiliares*/
            /*Obtener montos repartidos de área y subárea*/
        public ResponseGeneric<List<monto_repartido_cg>> Get_montos_repartidos(int p_opt, String p_id_capitulo_gasto_a_s, String p_id_item_a_s)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_capitulo_gasto_a_s", Tipo = "String", Valor = p_id_capitulo_gasto_a_s.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_item_a_s", Tipo = "String", Valor = p_id_item_a_s.ToString() });
                List<monto_repartido_cg> Lista = new List<monto_repartido_cg>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_monto_repartido_cg);
                            Lista = conexion.Query<monto_repartido_cg>().FromSql<monto_repartido_cg>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<monto_repartido_cg>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<monto_repartido_cg>>(ex);
            }
        }
            /*Validar si un capitulo de gasto existe de área, subárea y área subordinada*/
        public ResponseGeneric<List<capitulo_gasto_existente>> Validar_CG_existente(int p_opt, String p_capitulo_gasto_id, String p_item_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_capitulo_gasto_id", Tipo = "String", Valor = p_capitulo_gasto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_item_id", Tipo = "String", Valor = p_item_id.ToString() });
                List<capitulo_gasto_existente> Lista = new List<capitulo_gasto_existente>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_existe_capitulo_gasto);
                            Lista = conexion.Query<capitulo_gasto_existente>().FromSql<capitulo_gasto_existente>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<capitulo_gasto_existente>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<capitulo_gasto_existente>>(ex);
            }
        }
        /*Lista informativa de capitulos repartidos en todos lon niveles*/
        public ResponseGeneric<List<lista_info_capitulo_gasto>> Lista_info(int p_opt, String p_capitulo_gasto_id, String p_item_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_capitulo_gasto_id", Tipo = "String", Valor = p_capitulo_gasto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_item_id", Tipo = "String", Valor = p_item_id.ToString() });
                List<lista_info_capitulo_gasto> Lista = new List<lista_info_capitulo_gasto>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_lista_info_cg_repartidos);
                            Lista = conexion.Query<lista_info_capitulo_gasto>().FromSql<lista_info_capitulo_gasto>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<lista_info_capitulo_gasto>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<lista_info_capitulo_gasto>>(ex);
            }
        }
        /*Fin de auxiliares*/

    }
}
