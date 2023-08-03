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
    public class AsignacionOrganizacional_acceso_datos_core
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_dependencias_asignacion = "sp_get_dependencias_asignacion";
        string sp_get_areas_asignacion = "sp_get_areas_asignacion";
        string sp_get_lista_cap_gastos = "sp_get_lista_cap_gastos";
        string sp_partida_area = "sp_partida_area";
        string sp_get_existe_partida_area = "sp_get_existe_partida_area";
        string sp_get_monto_disponible_area = "sp_get_monto_disponible_area";

        public AsignacionOrganizacional_acceso_datos_core()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<estructura_tbl_dependencia>> Get_lista_dependencias(String id_instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = id_instancia.ToString() });
                List<estructura_tbl_dependencia> Lista = new List<estructura_tbl_dependencia>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_dependencias_asignacion);
                            Lista = conexion.Query<estructura_tbl_dependencia>().FromSql<estructura_tbl_dependencia>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<estructura_tbl_dependencia>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<estructura_tbl_dependencia>>(ex);
            }
        }
        public ResponseGeneric<List<estructura_tbl_area>> Get_lista_areas(String id_instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = id_instancia.ToString() });
                List<estructura_tbl_area> Lista = new List<estructura_tbl_area>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_areas_asignacion);
                            Lista = conexion.Query<estructura_tbl_area>().FromSql<estructura_tbl_area>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<estructura_tbl_area>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<estructura_tbl_area>>(ex);
            }
        }
        public ResponseGeneric<List<lista_cap_gastos_dep>> Get_lista_cap_gastos(String tipo,String id_dependencia, String id_instancia, String id_ejercicio)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_item", Tipo = "String", Valor = id_dependencia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = id_instancia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = id_ejercicio.ToString() });
                List<lista_cap_gastos_dep> Lista = new List<lista_cap_gastos_dep>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_lista_cap_gastos);
                            Lista = conexion.Query<lista_cap_gastos_dep>().FromSql<lista_cap_gastos_dep>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<lista_cap_gastos_dep>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_cap_gastos_dep>>(ex);
            }
        }
        public ResponseGeneric<List<lista_cap_gastos_areas>> Get_lista_cap_gastos_areas(String tipo, String id_area, String id_instancia, String id_ejercicio)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_item", Tipo = "String", Valor = id_area.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = id_instancia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = id_ejercicio.ToString() });
                List<lista_cap_gastos_areas> Lista = new List<lista_cap_gastos_areas>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_lista_cap_gastos);
                            Lista = conexion.Query<lista_cap_gastos_areas>().FromSql<lista_cap_gastos_areas>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<lista_cap_gastos_areas>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_cap_gastos_areas>>(ex);
            }
        }
        public ResponseGeneric<List<existe_partida>> Get_existe_partida(String id_area, String id_partida,String id_ejercicio)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = id_area.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_partida_id", Tipo = "String", Valor = id_partida.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = id_ejercicio.ToString() });
                List<existe_partida> Lista = new List<existe_partida>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_existe_partida_area);
                            Lista = conexion.Query<existe_partida>().FromSql<existe_partida>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<existe_partida>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<existe_partida>>(ex);
            }
        }
        public ResponseGeneric<List<CrudresponseIdentificador>> Add_Partida_areas(tbl_partida_area _tbl_partida_area)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = _tbl_partida_area.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_partida_area.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = _tbl_partida_area.p_tbl_area_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_partida_id", Tipo = "String", Valor = _tbl_partida_area.p_tbl_partida_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = _tbl_partida_area.p_tbl_ejercicio_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_propietario", Tipo = "String", Valor = _tbl_partida_area.p_id_propietario.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_planeado", Tipo = "String", Valor = _tbl_partida_area.p_monto_planeado.ToString() }); ;
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_asignado", Tipo = "String", Valor = _tbl_partida_area.p_monto_asignado.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_ejercido", Tipo = "String", Valor = _tbl_partida_area.p_monto_ejercido.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_devengado", Tipo = "String", Valor = _tbl_partida_area.p_monto_devengado.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus_partida", Tipo = "String", Valor = _tbl_partida_area.p_estatus_partida.ToString() });

                List<CrudresponseIdentificador> Lista = new List<CrudresponseIdentificador>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_partida_area);
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
        public ResponseGeneric<List<monto_asignacion>> Get_monto_asignado(int opt,String id_item,String id_partida, String id_ejercicio)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_item", Tipo = "String", Valor = id_item.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_partida_id", Tipo = "String", Valor = id_partida.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = id_ejercicio.ToString() });
                List<monto_asignacion> Lista = new List<monto_asignacion>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_monto_disponible_area);
                            Lista = conexion.Query<monto_asignacion>().FromSql<monto_asignacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<monto_asignacion>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<monto_asignacion>>(ex);
            }
        }

    }
}
